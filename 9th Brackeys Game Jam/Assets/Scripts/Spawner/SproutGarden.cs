using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutGarden : MonoBehaviour
{
    [SerializeField] private int population = 10;

    [SerializeField] private List<GameObject> sproutPrefabs;
    private int sproutIndex;
    [SerializeField] private Vector2 spawnArea;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < population; i++)
        {
            Vector2 randomPos = transform.localPosition +
                                new Vector3(Random.Range(-spawnArea.x / 2, spawnArea.x / 2), Random.Range(-spawnArea.y / 2, spawnArea.y / 2), 0f);

            sproutIndex = Random.Range(0, sproutPrefabs.Count);
            Instantiate(sproutPrefabs[sproutIndex], randomPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.localPosition, spawnArea);
    }
}
