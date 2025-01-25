using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien_Behaviour : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lifetime = 10f;
    private Vector2 movementDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        movementDirection = Random.insideUnitCircle.normalized;
        rb.velocity = movementDirection * moveSpeed;

        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 surfaceNormal = collision.contacts[0].normal;
        movementDirection = Vector2.Reflect(movementDirection, surfaceNormal);
        rb.velocity = movementDirection * moveSpeed;
    }
}