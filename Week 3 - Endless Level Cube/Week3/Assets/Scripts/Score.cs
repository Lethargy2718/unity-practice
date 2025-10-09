using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = Mathf.Floor(player.position.z / 10f).ToString();
    }
}
