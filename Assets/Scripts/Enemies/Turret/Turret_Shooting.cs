using UnityEngine;

public class Turret_Shooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootInterval = 3f;
    private Animator animator;

    public Transform leftFirePoint;
    public Transform rightFirePoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("Shoot", 1f);
    }

    void Shoot()
    {
        if (projectilePrefab != null)
        {
            animator.SetTrigger("isShooting");

            Instantiate(projectilePrefab, leftFirePoint.position, transform.rotation);
            Instantiate(projectilePrefab, rightFirePoint.position, transform.rotation);

            Invoke("Shoot", shootInterval);
        }
    }
}