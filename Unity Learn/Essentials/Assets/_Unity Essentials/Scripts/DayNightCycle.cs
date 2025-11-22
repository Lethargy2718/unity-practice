using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField]
    private float cycleLength = 3.0f;

    private float RotationPerSecond => 360f / cycleLength;

    public void Update()
    {
        transform.Rotate(Vector3.right, RotationPerSecond * Time.deltaTime);
    }
}
