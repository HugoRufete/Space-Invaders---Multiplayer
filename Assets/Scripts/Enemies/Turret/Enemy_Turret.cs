using UnityEngine;

public class Enemy_Turret : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player != null)
        {
            // Calculate direction to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Calculate the angle in Z-axis for 2D rotation
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            // Apply rotation only around Z-axis
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}