using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector]
    public bool gameEnded = false;
    public float restartDelay = 1.5f;
    public GameObject winUI;

    void Awake()
    {
        Instance = this;
    }

    public void Die()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            Invoke(nameof(Restart), 1.5f);
        }
    }

    public void WinLevel()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            winUI.SetActive(true);
        }
    }

    public void Restart()
    {
        gameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        gameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
