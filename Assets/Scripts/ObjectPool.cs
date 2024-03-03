using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] protected int Capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject prefab)
    {
        for (int i = 0; i < Capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(number => number.activeSelf == false);

        return result != null;
    }
}
