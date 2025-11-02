using UnityEngine;
using UnityEngine.InputSystem;
    
public class PlayerMovement : MonoBehaviour
{
    [Range(0, 500f)]
    public float forwardForce = 300f;

    [Range(0, 500f)]
    public float sidewaysForce = 200f;

    public Rigidbody rb;

    void FixedUpdate()
    {
        Vector3 force = Vector3.zero;

        force += Vector3.forward * forwardForce;

        if (Keyboard.current.dKey.isPressed)
        {
            force += Vector3.right * sidewaysForce;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            force += Vector3.left * sidewaysForce;
        }

        rb.AddForce(force * Time.fixedDeltaTime, ForceMode.VelocityChange);

        if (rb.position.y < -1f)
        {
            Die();
        }
    }

    public void Stop()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void Die()
    {
        GameManager.Instance.Die();
        enabled = false;
    }
}
