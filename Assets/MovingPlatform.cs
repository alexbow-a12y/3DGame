using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("How fast the platform moves back and forth.")]
    [SerializeField] private float moveSpeed = 2.0f;

    [Tooltip("How far the platform moves from its starting point.")]
    [SerializeField] private float moveDistance = 3.0f;

    [Tooltip("The axis to move along (e.g., Side-to-side = X).")]
    [SerializeField] private Vector3 moveDirection = Vector3.right;

    [Header("Rotation Settings")]
    [Tooltip("Degrees per second the platform spins.")]
    [SerializeField] private float rotationSpeed = 45.0f;

    [Tooltip("The axis to spin around (Y = 1 spins like a record).")]
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        moveDirection.Normalize();
        rotationAxis.Normalize();
    }

    void Update()
    {
        // 1. Handle Movement (Translation)
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = startPosition + (moveDirection * offset);

        // 2. Handle Rotation (Spin)
        // We use Time.deltaTime here for smooth rotation per frame
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }

    // --- Player Parenting Logic ---


    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object we hit is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Make the player a child of the platform
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the object leaving is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Detach the player
            collision.transform.SetParent(null);
        }
    }
}