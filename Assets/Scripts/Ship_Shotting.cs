using UnityEngine;

public class Ship_Shotting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public Transform leftFirePoint;   // Left projectile spawn point
    public Transform rightFirePoint;  // Right projectile spawn point
    public GameObject projectilePrefab; // Projectile prefab to instantiate

    [SerializeField] private float fireRate = 0.5f; // Time between shots
    private float nextFireTime = 0f;

    void Update()
    {
        // Check if Space key is pressed and enough time has passed since last shot
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Verify all necessary references are set
        if (leftFirePoint == null || rightFirePoint == null || projectilePrefab == null)
        {
            Debug.LogWarning("Shooting references are not set properly!");
            return;
        }

        // Instantiate projectiles from both fire points
        Instantiate(projectilePrefab, leftFirePoint.position, leftFirePoint.rotation);
        Instantiate(projectilePrefab, rightFirePoint.position, rightFirePoint.rotation);
    }
}