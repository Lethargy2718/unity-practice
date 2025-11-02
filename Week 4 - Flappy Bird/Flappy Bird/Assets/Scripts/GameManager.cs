using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Player player;
    private Parallax parallax;

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

    [Header("UI Elements")]
    public GameObject playBtn;
    public GameObject gameOver;

    private void Awake()
    {
        Instance = this;
        player = FindAnyObjectByType<Player>();
        parallax = FindAnyObjectByType<Parallax>();
        Pause();

    }

    private void Start()
    {
        initialSpeed = scrollSpeed;
    }

    public void Play()
    {
        ScoreManager.Instance.ResetScore();

        gameOver.SetActive(false);
        playBtn.SetActive(false);

        Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);
        foreach (Pipes pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }

        Unpause();
    }

    public void GameOver()
    {
        Pause();
        gameOver.SetActive(true);

        StartCoroutine(ShowPlayBtnCoroutine());

        IEnumerator ShowPlayBtnCoroutine()
        {
            yield return new WaitForSecondsRealtime(1f);
            playBtn.SetActive(true);
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        if (player != null) player.enabled = false;
        if (parallax != null) parallax.enabled = false;
    }

    private void Unpause()
    {
        Time.timeScale = 1f;
        if (player != null) player.enabled = true;
        if (parallax != null) parallax.enabled = true;
    }

    public void SetDifficulty(float score)
    {
        // Ramp up quickly at first then flatten to prevent unplayable late-game speeds
        scrollSpeed = Mathf.Clamp(
            initialSpeed + Mathf.Log10(score / scale + 1) * gain,
            initialSpeed,
            2f
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