using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ghostPrefab;
    [SerializeField] private List<Transform> _spawnEnemy;
    [SerializeField] private float _setSpawnTimer = 3f;
    private float _spawnTimer;
    private int _spawnIndex;

    // Start is called before the first frame update
    void Start()
    {
        _spawnTimer = _setSpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0f)
        {
            _spawnIndex = Random.Range(0, _spawnEnemy.Count);
            Instantiate(_ghostPrefab, _spawnEnemy[_spawnIndex].position, Quaternion.identity);
            _spawnTimer = _setSpawnTimer;
        }
    }
}
