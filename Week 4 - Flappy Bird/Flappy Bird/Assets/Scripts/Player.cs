using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    private Camera mainCamera;

    private SpriteRenderer sr;
    public Sprite[] sprites;
    private int spriteIndex = 0;
    public float speedMin = 5f;
    public float speedMax = 25f;
    private float yMin;
    private float yMax;

    private Rigidbody2D rb;
    public float strength = 5f;
    public float gravityScale = 1f;
    private bool flap;


    private void Awake()
    {
        mainCamera = Camera.main;
        SetAnimationSpeed();

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        sr = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        StartCoroutine(AnimateSpriteCoroutine());
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame
            || Mouse.current.leftButton.wasPressedThisFrame
            || (Touchscreen.current?.primaryTouch.press.wasPressedThisFrame ?? false))
        {
            flap = true;
        }

        SetAnimationSpeed();
    }

    private void FixedUpdate()
    {
        if (flap)
        {
            Vector2 velocity = rb.linearVelocity;
            velocity.y = 0f;
            rb.linearVelocity = velocity;
            rb.AddForce(Vector2.up * strength, ForceMode2D.Impulse);
            flap = false;
        }
    }

    private IEnumerator AnimateSpriteCoroutine()
    {
        while (true)
        {
            spriteIndex = (spriteIndex + 1) % sprites.Length;
            sr.sprite = sprites[spriteIndex];

            yield return new WaitForSeconds(SecondsPerFrame());
        }
    }

    private float SecondsPerFrame()
    {
        float t = Mathf.InverseLerp(yMax, yMin, rb.position.y);
        float fps = Mathf.Lerp(speedMin, speedMax, t);
        return 1f / fps;
    }

    private void SetAnimationSpeed()
    {
        if (!mainCamera) return;
        yMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }
}
