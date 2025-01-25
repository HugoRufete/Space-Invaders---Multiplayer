using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Alien_Instantiator : MonoBehaviour
{
    public GameObject alienPrefab;
    public List<Transform> spawnPoints;

    private int rightClickCount = 0;
    public float cooldownTime = 3f;
    private float lastSpawnTime;
    private bool cooldownActive = false;

    public int maxAlienCount = 3;
    public int alienRemaining;

    // UI Variable for cooldown feedback
    public Image cooldownFillImage;
    public TMP_Text alienRemaining_Text;

    private void Start()
    {
        alienRemaining = maxAlienCount;

        // Ensure the fill image starts full
        if (cooldownFillImage != null)
        {
            cooldownFillImage.fillAmount = 1;
        }
    }

    void Update()
    {
        alienRemaining_Text.text = alienRemaining.ToString();

        if (Input.GetMouseButtonDown(1))
        {
            if (!cooldownActive || rightClickCount < maxAlienCount)
            {
                if (alienPrefab != null && spawnPoints.Count > 0)
                {
                    Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                    Instantiate(alienPrefab, randomSpawnPoint.position, Quaternion.identity);

                    rightClickCount++;
                    alienRemaining--;

                    if (rightClickCount >= maxAlienCount)
                    {
                        cooldownActive = true;
                        lastSpawnTime = Time.time;

                        // Start cooldown feedback
                        if (cooldownFillImage != null)
                        {
                            cooldownFillImage.fillAmount = 0;
                        }

                        StartCoroutine(CooldownFill());
                    }
                }
            }
        }
    }

    private IEnumerator CooldownFill()
    {
        float elapsedTime = 0;

        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            if (cooldownFillImage != null)
            {
                cooldownFillImage.fillAmount = Mathf.Clamp01(elapsedTime / cooldownTime);
            }
            yield return null;
        }

        // Reset after cooldown
        cooldownActive = false;
        rightClickCount = 0;
        alienRemaining = maxAlienCount;

        if (cooldownFillImage != null)
        {
            cooldownFillImage.fillAmount = 1; // Ensure it's full after cooldown ends
        }
    }
}
