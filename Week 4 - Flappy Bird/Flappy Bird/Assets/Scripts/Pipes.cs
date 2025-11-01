using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speedMultiplier = 2f;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;        
    }
    void Update()
    {
        transform.position += GameManager.Instance.scrollSpeed * Time.deltaTime * speedMultiplier * Vector3.left;
        CheckBounds();
    }

    private void CheckBounds()
    {
        double left = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - 10.0;
        if (transform.position.x < left)
        {
            Destroy(gameObject);
        }
    }
}
