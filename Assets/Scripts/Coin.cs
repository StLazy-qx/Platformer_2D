using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _reward = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Wallet wallet))
        {
            wallet.AddMoney(_reward);

            Destroy(gameObject);
        }
    }
}
