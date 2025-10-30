using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool flap;
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
        if (Keyboard.current.spaceKey.wasPressedThisFrame
            || Mouse.current.leftButton.wasPressedThisFrame
            || (Touchscreen.current?.primaryTouch.press.wasPressedThisFrame ?? false))
        {
            flap = true;
        }
    }

    private void FixedUpdate()
    {
        if (flap)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * strength, ForceMode2D.Impulse);
            flap = false;
        }
    }
}
