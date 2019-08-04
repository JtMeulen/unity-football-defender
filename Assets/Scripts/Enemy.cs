using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Details")]
    [SerializeField] float health = 30f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float VFXDuration = 1f;

    [Header("Shooting Config")]
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float shootingSpeed = 10f;
    [SerializeField] GameObject shootingObject;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;

    private float shotCounter;

    private void Start()
    {
        SetShotCounter();
    }

    private void Update()
    {
        CountDownAndShoot();   
    }

    private void SetShotCounter()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Shoot();
            SetShotCounter();
        }
    }

    private void Shoot()
    {
        GameObject enemyShootingObj = Instantiate(
            shootingObject,
            transform.position,
            shootingObject.transform.rotation) as GameObject;
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position);
        enemyShootingObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shootingSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();

        damageDealer.Hit();
        AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, 0.75f);

        if (health <= 0)
        {
            GameObject particleEffect = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(particleEffect, VFXDuration);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
