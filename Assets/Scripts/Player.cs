using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Customizable variables
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float gameSpacePadding = 1f;

    // Variable init
    float minX;
    float maxX;
    float minY;
    float maxY;

    private void Start()
    {
        SetUpWorldBoundaries();
    }

    private void SetUpWorldBoundaries()
    {
        Camera mainCamera = Camera.main;
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + gameSpacePadding;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - gameSpacePadding;
        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + gameSpacePadding;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - gameSpacePadding;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        float newXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);

        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        transform.position = new Vector2(newXPos, newYPos);
    }
}
