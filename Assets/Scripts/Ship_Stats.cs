using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Stats : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            print("Player Dead");
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
    }
}
