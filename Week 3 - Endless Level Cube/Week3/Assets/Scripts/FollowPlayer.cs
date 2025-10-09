using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position;
        offset.z *= 2;
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset; 
        }
    }
}
