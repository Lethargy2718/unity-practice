using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector]
    public bool gameEnded = false;
    public float restartDelay = 0.5f;
    public GameObject winUI;

    [Header("Difficulty Settings")]
    [Range(0.3f, 1.0f)]
    [Tooltip("Initial speed.")]
    public float scrollSpeed = 0.6f;

    [Tooltip("Horizontal stretch of the difficulty curve.")]
    public float scale = 200f; // higher scale => same curve on a wider range

    [Tooltip("Difficulty growth speed.")]
    public float gain = 5f; // higher gain => faster growth

    private float initialSpeed;
    private int score = 0;

    void Awake()
    {
        Instance = this;
        initialSpeed = scrollSpeed;
    }

    public void IncreaseScore()
    {
        score++;
        Debug.Log(score);

        // Ramp up quickly at first then flatten to prevent unplayable late-game speeds
        scrollSpeed = Mathf.Clamp(
            initialSpeed + Mathf.Log10(score / scale + 1) * gain,
            initialSpeed,
            2f
        );

        Debug.Log(scrollSpeed);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        StartCoroutine(RestartCoroutine());

        IEnumerator RestartCoroutine()
        {
            yield return new WaitForSecondsRealtime(restartDelay);
            Restart();
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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