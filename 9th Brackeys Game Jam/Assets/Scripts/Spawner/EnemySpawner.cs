using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyPrefabs;
    private int _enemyIndex;
    [SerializeField] private List<Transform> _spawnAreas;
    private int _spawnIndex;
    [SerializeField] private float _setSpawnTimer = 3f;
    private float _spawnTimer;
    [SerializeField] private int _setSpawnLimit = 5;
    private int _spawnCount = 0;
    public void ReduceSpawnCount() => _spawnCount--;

    // Start is called before the first frame update
    void Start()
    {
        _spawnTimer = _setSpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnCount < _setSpawnLimit)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0f)
        {
            _enemyIndex = Random.Range(0, _enemyPrefabs.Count);
            _spawnIndex = Random.Range(0, _spawnAreas.Count);

            Instantiate(_enemyPrefabs[_enemyIndex], _spawnAreas[_spawnIndex].position, Quaternion.identity);
            _spawnCount++;
            _spawnTimer = _setSpawnTimer;
        }
    }
}
