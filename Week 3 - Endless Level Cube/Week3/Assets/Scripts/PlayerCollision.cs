using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            playerMovement.Stop();
            playerMovement.enabled = false;
            GameManager.Instance.EndGame();
        }
    }
}
