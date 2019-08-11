using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration variables
    [Header("Player Details")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] int health = 50;
    [SerializeField] float gameSpacePadding = 1f;

    [Header("Shooting Config")]
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float ballSpeed = 20f;
    [SerializeField] float ballCooldown = .3f;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;

    // Variable init
    float minX;
    float maxX;
    float minY;
    float maxY;
    Coroutine throwBallCoroutine;
    LifeText lifeText;

    private void Start()
    {
        if(!ballPrefab)
        {
            Debug.LogError("Could not find ball prefab on player");
        }

        lifeText = FindObjectOfType<LifeText>();

        lifeText.UpdateHealthText(health);
        SetUpWorldBoundaries(); 
    }

    private void Update()
    {
        Move();
        ThrowBall();
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        float newXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);

        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void ThrowBall()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            throwBallCoroutine = StartCoroutine(ThrowBallCoroutine());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(throwBallCoroutine);
        }
    }

    IEnumerator ThrowBallCoroutine()
    {
        while(true)
        {
            GameObject ball = Instantiate(
                ballPrefab,
                transform.position,
                ballPrefab.transform.rotation) as GameObject;
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ballSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
            yield return new WaitForSeconds(ballCooldown);
        }
    }

    private void SetUpWorldBoundaries()
    {
        Camera mainCamera = Camera.main;
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + gameSpacePadding;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - gameSpacePadding;
        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + gameSpacePadding;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - gameSpacePadding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        lifeText.UpdateHealthText(health);

        damageDealer.Hit();
        AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);

        if (health <= 0)
        {
            lifeText.UpdateHealthText(0);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
            FindObjectOfType<Level>().LoadGameOver();

			FindObjectOfType<GameSession>().StopScore();
		}
    }
}
