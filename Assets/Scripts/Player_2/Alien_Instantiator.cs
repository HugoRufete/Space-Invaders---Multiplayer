using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien_Instantiator : MonoBehaviour
{
    public GameObject alienPrefab; // Prefab to instantiate
    public List<Transform> spawnPoints; // List of possible spawn locations

    void Update()
    {
        // Check for right mouse button click
        if (Input.GetMouseButtonDown(1)) // 1 represents right mouse button
        {
            // Ensure prefab and spawn points are set
            if (alienPrefab != null && spawnPoints.Count > 0)
            {
                // Select random spawn point from list
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

                // Instantiate prefab at selected spawn point
                Instantiate(alienPrefab, randomSpawnPoint.position, Quaternion.identity);
            }
        }
    }
}