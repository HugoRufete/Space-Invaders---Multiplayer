using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ship_Stats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Image healthBarFill;

    private SpriteRenderer shipRenderer;

    public GameObject gameOverObject;

    public UnityEvent onGameOver;

    void Start()
    {
        shipRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        UpdateHealthBar();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            onGameOver.Invoke();
            gameOverObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Damage(20);
        }
        else if (collision.CompareTag("Enemy_Projectile"))
        {
            Damage(10);
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHealthBar();

        StartCoroutine(FlashRed());
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    IEnumerator FlashRed()
    {
        shipRenderer.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        shipRenderer.color = Color.white;
    }

    public void ReloadScene()
    {
        print("recargando escena");
        SceneManager.LoadScene("SampleScene");
    }
}
