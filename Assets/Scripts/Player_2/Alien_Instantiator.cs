using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien_Instantiator : MonoBehaviour
{
    public GameObject alienPrefab;
    public List<Transform> spawnPoints;

    private int rightClickCount = 0;
    public float cooldownTime = 3f;
    private float lastSpawnTime;
    private bool cooldownActive = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!cooldownActive || rightClickCount < 3)
            {
                if (alienPrefab != null && spawnPoints.Count > 0)
                {
                    Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                    Instantiate(alienPrefab, randomSpawnPoint.position, Quaternion.identity);

                    rightClickCount++;

                    if (rightClickCount >= 3)
                    {
                        cooldownActive = true;
                        lastSpawnTime = Time.time;
                    }
                }
            }

            if (cooldownActive && Time.time >= lastSpawnTime + cooldownTime)
            {
                cooldownActive = false;
                rightClickCount = 0;
            }
        }
    }
}