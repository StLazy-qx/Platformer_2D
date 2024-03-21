using System.Collections;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private GameObject _template;

    //private float _timeBetweenSpawnCoin = 0.01f;
    private Transform[] _points;

    private void Start()
    {
        Initialize(_template);

        _points = new Transform[_spawnPoints.childCount];

        for (int i = 0; i < _spawnPoints.childCount; i++)
        {
            Transform child = _spawnPoints.GetChild(i);
            SpawnPoint spawnPoint = child.GetComponent<SpawnPoint>();

            if (spawnPoint != null)
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
            for (int i = 0; i < _points.Length; i++)
            {
                if (TryGetObject(out GameObject coin))
                {
                    Transform spawnPoint = _points[i];
                    SetCoin(coin, spawnPoint.position);
                }
                else
                {
                    yield break;
                }
            }

            yield break;
        }
    }

    private void SetCoin(GameObject coin, Vector2 spawnPoint)
    {
        coin.SetActive(true);
        coin.transform.position = spawnPoint;
    }
}
