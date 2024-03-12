using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    [SerializeField] private Player _player;

    private int _money;

    public UnityAction<int> MoneyChanged;

    private void Start()
    {
        MoneyChanged?.Invoke(_money);
    }

    public void AddMoney(int coin)
    {
        _money += coin;

        MoneyChanged?.Invoke(_money);
    }
}
