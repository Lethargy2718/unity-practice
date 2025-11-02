using UnityEngine;

public class WinPanel : MonoBehaviour
{
    public void Win()
    {
        GameManager.Instance.NextLevel();
    }
}
