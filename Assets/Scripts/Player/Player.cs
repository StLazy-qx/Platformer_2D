using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(CharacterGlobalCollisionHandler))]

public class Player : MonoBehaviour 
{
    private CharacterGlobalCollisionHandler _collisionHandler;
    private Health _health;
    private Wallet _wallet;

    private void Start()
    {
        _collisionHandler = GetComponent<CharacterGlobalCollisionHandler>();
        _health = GetComponent<Health>();
        _wallet = GetComponent<Wallet>();
        _collisionHandler.CoinCollisionEntered += OnCoinCollision;
        _collisionHandler.MedicineChestCollisionEntered += OnMedicineChestCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.CoinCollisionEntered -= OnCoinCollision;
        _collisionHandler.MedicineChestCollisionEntered -= OnMedicineChestCollision;
    }

    private void OnCoinCollision(Coin coin)
    {
        _wallet.AddMoney(coin.Reward);
        Destroy(coin.gameObject);
    }

    private void OnMedicineChestCollision(HealingItem healingItem)
    {
        _health.TakeHeal(healingItem.HealAmount);
        Destroy(healingItem.gameObject);
    }
}