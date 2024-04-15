using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private Transform _template;
    [SerializeField] private Transform _container;

    private Transform[] _points;

    private void Start()
    {
        SpawnPoint[] spawnPoints = _spawnPointsParent.GetComponentsInChildren<SpawnPoint>();
        _points = new Transform[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            _points[i] = spawnPoints[i].transform; 
        }

        StartCoroutine(SpawnCoinsPeriodically());
    }

    private IEnumerator SpawnCoinsPeriodically()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            var template = Instantiate(_template, _container.transform);
            template.transform.position = _points[i].position;
        }

        yield break;
    }
}
