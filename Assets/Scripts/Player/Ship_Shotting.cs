using UnityEngine;

public class Ship_Shotting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public Transform leftFirePoint;   
    public Transform rightFirePoint;  
    public GameObject projectilePrefab; 

    [SerializeField] private float fireRate = 0.5f; 
    private float nextFireTime = 0f;

    public Audio_Manager audioManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        audioManager.ShipShooting();

        if (leftFirePoint == null || rightFirePoint == null || projectilePrefab == null)
        {
            Debug.LogWarning("Shooting references are not set properly!");
            return;
        }

        Instantiate(projectilePrefab, leftFirePoint.position, leftFirePoint.rotation);
        Instantiate(projectilePrefab, rightFirePoint.position, rightFirePoint.rotation);
    }
}