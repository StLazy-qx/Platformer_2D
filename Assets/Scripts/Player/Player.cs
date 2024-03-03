using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 10;

    public int Money { get; private set; }

    public UnityAction<int> _MoneyView;

    private void Start()
    {
        _MoneyView?.Invoke(Money);
    }

    public void AddMoney(int coin)
    {
        Money += coin;

        _MoneyView?.Invoke(Money);
    }
}
