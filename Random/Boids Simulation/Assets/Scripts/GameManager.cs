using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static Boid[] boids;
    public static Camera mainCamera;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
    }

    private void Start()
    {
        boids = FindObjectsByType<Boid>(
            findObjectsInactive: FindObjectsInactive.Include,
            sortMode: FindObjectsSortMode.None
        );
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