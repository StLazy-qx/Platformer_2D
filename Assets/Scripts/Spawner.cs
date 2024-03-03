using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private GameObject _template;

    private float _timeBetweenSpawnCoin = 2f;
    private Transform[] _points;
    private List<int> _occupiedPositions = new List<int>();
    private List<int> _freeSpawnPoints = new List<int>();

    private void Start()
    {
        Initialize(_template);

        _points = new Transform[_spawnPoints.childCount];

        for (int i = 0; i < _spawnPoints.childCount; i++)
        {
            Transform child = _spawnPoints.GetChild(i);

            if (child.CompareTag("SpawnPoint"))
            {
                _points[i] = child;
            }
        }

        StartCoroutine(SpawnCoinsPeriodically());
    }

    private IEnumerator SpawnCoinsPeriodically()
    {
        while (true)
        {
            _freeSpawnPoints = GetFreeSpawnPoints();

            if (TryGetObject(out GameObject coin))
            {
                if (_freeSpawnPoints.Count > 0)
                {
                    int spawnPointNumber = Random.Range(0, _freeSpawnPoints.Count);
                    int selectedSpawnPointIndex = _freeSpawnPoints[spawnPointNumber];
                    SetCoin(coin, _points[selectedSpawnPointIndex].position);
                    _occupiedPositions.Add(selectedSpawnPointIndex);
                    _freeSpawnPoints.RemoveAt(spawnPointNumber);
                }
            }

            yield return new WaitForSeconds(_timeBetweenSpawnCoin);
        }
    }

    private void SetCoin(GameObject coin, Vector2 spawnPoint)
    {
        coin.SetActive(true);
        coin.transform.position = spawnPoint;
    }

    private List<int> GetFreeSpawnPoints()
    {
        List<int> freeSpawnPoints = new List<int>();

        for (int i = 0; i < _spawnPoints.childCount; i++)
        {
            if (!_occupiedPositions.Contains(i))
            {
                freeSpawnPoints.Add(i);
            }
        }

        return freeSpawnPoints;
    }
}
