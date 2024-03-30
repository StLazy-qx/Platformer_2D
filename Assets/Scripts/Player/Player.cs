using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterGlobalCollisionHandler))]
[RequireComponent(typeof(Wallet))]

public class Player : MonoBehaviour
{
    private CharacterGlobalCollisionHandler _collisionHandler;
    private Wallet _wallet;

    public Transform CurrentPosition => transform;

    private void Start()
    {
        _collisionHandler = GetComponent<CharacterGlobalCollisionHandler>();
        _wallet = GetComponent<Wallet>();
        _collisionHandler.OnCoinCollisionEnter += CoinCollisionHandler;
    }

    public void CoinCollisionHandler(Coin coin)
    {
        _wallet.AddMoney(coin.Reward);
    }
}
