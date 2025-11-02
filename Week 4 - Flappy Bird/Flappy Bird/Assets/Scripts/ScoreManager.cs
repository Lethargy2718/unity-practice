using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public TextMeshProUGUI scoreText;

    private int score;
    private int Score
    {
        get => score;
        set
        {
            score = value;
            if (scoreText != null) scoreText.text = score.ToString();
            GameManager.Instance.SetDifficulty(score);

        }
    }
    private void Awake()
    {
        Instance = this;
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public void IncreaseScore()
    {
        Score++;
    }
}
