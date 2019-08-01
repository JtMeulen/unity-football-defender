using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 30f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        health -= collision.gameObject.GetComponent<DamageDealer>().GetDamage();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
