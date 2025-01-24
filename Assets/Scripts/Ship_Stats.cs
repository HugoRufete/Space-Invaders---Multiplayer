using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship_Stats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Sprite[] healthSprites;
    public Image uiHealthImage;
    public SpriteRenderer shipRenderer; // Reference to the ship's sprite renderer

    void Start()
    {
        currentHealth = maxHealth;
        UpdateSprite();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            print("Player Dead");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Damage(20);
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        UpdateSprite();

        // Start coroutine to flash red
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        // Change color to red
        shipRenderer.color = Color.red;

        // Wait for 1 second
        yield return new WaitForSeconds(0.5f);

        // Return to original color
        shipRenderer.color = Color.white;
    }

    void UpdateSprite()
    {
        float healthPercentage = (float)currentHealth / maxHealth;

        int spriteIndex = Mathf.Clamp(healthSprites.Length - 1 - Mathf.FloorToInt(healthPercentage * healthSprites.Length), 0, healthSprites.Length - 1);

        if (uiHealthImage != null)
        {
            uiHealthImage.sprite = healthSprites[spriteIndex];
        }
    }
}