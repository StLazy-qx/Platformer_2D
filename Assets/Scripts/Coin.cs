using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _reward = 1;

    public int Reward => _reward;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Wallet wallet))
        {
            Destroy();

            wallet.AddMoney(_reward);
        }
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }
}
