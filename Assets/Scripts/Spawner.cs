using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private GameObject _template;

    private float _timeBetweenSpawnCoin = 2f;
    private Transform[] _points;

    private List<Transform> _freeSpawnPoints = new List<Transform>();

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
            List<Transform> occupiedPoints = GetObjectPosition().Select(obj => obj.transform).ToList();
            List<Transform> freeSpawnPoints = _points.ToList().Except(occupiedPoints).ToList();

            if (TryGetObject(out GameObject coin))
            {
                int randomIndex = Random.Range(0, freeSpawnPoints.Count);
                Transform spawnPoint = freeSpawnPoints[randomIndex];
                SetCoin(coin, spawnPoint.position);
            }

            yield return new WaitForSeconds(_timeBetweenSpawnCoin);
        }
    }

    private void SetCoin(GameObject coin, Vector2 spawnPoint)
    {
        coin.SetActive(true);
        coin.transform.position = spawnPoint;
    }
}
