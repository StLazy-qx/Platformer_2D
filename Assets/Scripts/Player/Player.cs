using UnityEngine;

[RequireComponent(typeof(CharacterGlobalCollisionHandler))]
[RequireComponent(typeof(Wallet))]

public class Player : MonoBehaviour
{
    private CharacterGlobalCollisionHandler _collisionHandler;
    private Wallet _wallet;

    private void Start()
    {
        _collisionHandler = GetComponent<CharacterGlobalCollisionHandler>();
        _wallet = GetComponent<Wallet>();
        _collisionHandler.CoinCollisionEnter += CoinCollisionHandler;
    }

    public void CoinCollisionHandler(Coin coin)
    {
        _wallet.AddMoney(coin.Reward);
    }
}
