using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Movement speed in units per second")]
    [Range(0f, 50f)]
    public float speed = 10f;

    void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.forward;
    }
}
