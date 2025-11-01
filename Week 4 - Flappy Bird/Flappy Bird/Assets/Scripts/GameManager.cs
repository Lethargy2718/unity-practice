using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector]
    public bool gameEnded = false;
    public float restartDelay = 1.5f;
    public GameObject winUI;

    public float scrollSpeed = 0.6f;


    void Awake()
    {
        Instance = this;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}