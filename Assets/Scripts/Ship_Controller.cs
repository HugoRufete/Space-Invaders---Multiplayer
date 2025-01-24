using UnityEngine;

public class Ship_Controller : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float rotationSpeed = 100f;

    private Vector2 movement;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movement = new Vector2(horizontalInput, verticalInput).normalized;

        // Rotation input
        float rotationInput = 0f;
        if (Input.GetKey(KeyCode.E)) rotationInput = -1f; 
        if (Input.GetKey(KeyCode.Q)) rotationInput = 1f;  

        // Apply rotation
        transform.Rotate(Vector3.forward * rotationInput * rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, movement * moveSpeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
    }
}