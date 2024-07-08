using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    private int _money;

    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        MoneyChanged?.Invoke(_money);
    }

    public void AddMoney(int value)
    {
        _money += value;
        MoneyChanged?.Invoke(_money);
    }
}
