using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Material background;
    public float speedMultiplier = 1f;

    void Start()
    {
        background = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float globalSpeed = GameManager.Instance.scrollSpeed;
        background.mainTextureOffset += new Vector2(globalSpeed * speedMultiplier * Time.deltaTime, 0);
    }
}
