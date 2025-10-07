using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 50f)]
    public float forwardSpeed = 10f;

    [Range(0, 50f)]
    public float sidewaysSpeed = 5f;

    public Rigidbody rb;

    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;

        velocity += Vector3.forward * forwardSpeed;

        if (Keyboard.current.dKey.isPressed)
        {
            velocity += Vector3.right * sidewaysSpeed;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            velocity += Vector3.left * sidewaysSpeed;
        }

        rb.linearVelocity = velocity;
    }
}
