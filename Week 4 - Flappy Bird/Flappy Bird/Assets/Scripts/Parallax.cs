using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Material background;

    public float scrollSpeedX = 0.4f;
    private Vector2 offset;

    void Start()
    {
        background = GetComponent<Renderer>().material;
        offset = background.mainTextureOffset;
    }

    void Update()
    {
        offset.x += scrollSpeedX * Time.deltaTime;
        background.mainTextureOffset = offset;
    }
}
