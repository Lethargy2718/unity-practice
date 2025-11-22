using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float speed = 5f;
    public bool canMoveDiagonally = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Get raw input
        Vector2 input = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 movement = input;
        
        if (!canMoveDiagonally)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                movement.y = 0;
            }
            else
            {
                movement.x = 0; 
            }
        }

        movement = movement.normalized;

        // Move player
        rb.linearVelocity = movement * speed;

        // Rotate player toward movement direction
        if (movement.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
