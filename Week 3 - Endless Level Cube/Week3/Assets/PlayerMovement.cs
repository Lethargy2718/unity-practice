using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Movement speed in units per second")]
    [Range(0f, 50f)]
    public float speed = 10f;
    public Rigidbody rb;

    void FixedUpdate()
    {
        Vector3 movement = speed * Time.fixedDeltaTime * Vector3.forward;
        rb.MovePosition(rb.position + movement);
    }
}
