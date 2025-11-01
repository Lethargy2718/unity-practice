using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    private Camera mainCamera;

    [Header("Animation Settings")]
    public Sprite[] sprites;
    public float animationMinSpeed = 5f;
    public float animationMaxSpeed = 25f;
    private SpriteRenderer sr;
    private int spriteIndex = 0;
    private float yMin;
    private float yMax;

    [Header("Physics Settings")]
    public float strength = 5f;
    public float gravityScale = 1f;
    private Rigidbody2D rb;
    private bool flap;

    [Header("Rotation Settings")]
    public float maxRotation = 30f;
    public float rotationSpeed = 5f;
    public float rotationSensitivity = 5f;


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

        HandleRotation();
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
        float fps = Mathf.Lerp(animationMinSpeed, animationMaxSpeed, t);
        return 1f / fps;
    }

    private void SetAnimationSpeed()
    {
        if (!mainCamera) return;
        yMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void HandleRotation()
    {
        float targetRotation = Mathf.Clamp(rb.linearVelocity.y * rotationSensitivity, -maxRotation, maxRotation);
        float zRotation = Mathf.LerpAngle(transform.eulerAngles.z, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
    }
}
