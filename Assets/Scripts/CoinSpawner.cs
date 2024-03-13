using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoints;
    [SerializeField] private Transform _container;
    [SerializeField] private Coin _template;

    private float _timeBetweenSpawnCoin = 0.01f;
    private Transform[] _points;

    private void Start()
    {
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
            for (int i = 0; i < _points.Length; i++)
            {
                Coin spawnCoin = Instantiate(_template, _container.transform);
                spawnCoin.transform.position = _points[i].position;
            }

            yield return new WaitForSeconds(_timeBetweenSpawnCoin);
        }
    }
}
