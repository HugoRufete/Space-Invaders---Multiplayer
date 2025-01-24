using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{

    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth = 40;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            DoDamage(10);
        }
    }

    public void DoDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        print("Enemy Damaged");
    }
}
