using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool flapInput;

    public float strength = 5f;
    public float gravityScale = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void Update()
    {
        flapInput = Keyboard.current.spaceKey.wasPressedThisFrame
                    || Mouse.current.leftButton.wasPressedThisFrame
                    || (Touchscreen.current?.primaryTouch.press.wasPressedThisFrame ?? false);
    }

    private void FixedUpdate()
    {
        if (flapInput)
        {
            Vector2 v = rb.linearVelocity;
            v.y = 0f;
            rb.linearVelocity = v;

            rb.AddForce(Vector2.up * strength, ForceMode2D.Impulse);

            flapInput = false;
        }
    }
}
