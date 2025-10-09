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

    public void EndGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            Debug.Log("YOU DIED");
            Invoke(nameof(Restart), 1.5f);
        }
    }

    public void WinLevel()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            Debug.Log("YOU WON");
            winUI.SetActive(true);
        }
    }

    public void Restart()
    {
        gameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
