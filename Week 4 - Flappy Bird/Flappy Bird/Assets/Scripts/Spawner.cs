using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    private Coroutine spawnCoroutine;

    public float pipeSpacing = 2f;
    public float minHeight = -1f;
    public float maxHeight = 1f;

    private void OnEnable()
    {
        spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable()
    {
        if (spawnCoroutine != null) 
            StopCoroutine(spawnCoroutine);
    }

    private IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.y += Random.Range(minHeight, maxHeight);
            Instantiate(prefab, spawnPos, Quaternion.identity);
            float delay = pipeSpacing / GameManager.Instance.scrollSpeed;
            yield return new WaitForSeconds(delay);
        }
    }
}