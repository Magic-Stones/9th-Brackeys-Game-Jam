using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutGarden : MonoBehaviour
{
    [SerializeField] private int population = 10;
    [SerializeField] private int instantiates = 0;

    [SerializeField] private List<GameObject> sproutPrefabs;
    private int sproutIndex;
    [SerializeField] private Vector2 spawnArea;

    [SerializeField] private float greatTreeZone = 1f;
    [SerializeField] private LayerMask sproutLayer;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < population; i++)
        {
            Vector2 randomPos = transform.localPosition +
                                new Vector3(Random.Range(-spawnArea.x / 2, spawnArea.x / 2), Random.Range(-spawnArea.y / 2, spawnArea.y / 2), 0f);

            sproutIndex = Random.Range(0, sproutPrefabs.Count);
            GameObject sprout = Instantiate(sproutPrefabs[sproutIndex], randomPos, Quaternion.identity);
            instantiates++;

            Collider2D[] plants = Physics2D.OverlapCircleAll(transform.localPosition, greatTreeZone, sproutLayer);
            foreach (Collider2D plant in plants)
            {
                i--;
                Destroy(sprout);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.localPosition, spawnArea);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.localPosition, greatTreeZone);
    }
}
