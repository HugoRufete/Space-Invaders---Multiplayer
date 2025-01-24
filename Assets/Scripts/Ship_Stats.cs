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
    private SpriteRenderer shipRenderer;

    void Start()
    {
        shipRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        UpdateSprite();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
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

        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        shipRenderer.color = Color.red;

        yield return new WaitForSeconds(0.5f);

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