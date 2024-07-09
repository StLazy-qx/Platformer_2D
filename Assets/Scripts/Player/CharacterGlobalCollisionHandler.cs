using UnityEngine;
using UnityEngine.Events;

public class CharacterGlobalCollisionHandler : MonoBehaviour
{
    public event UnityAction<Coin> CoinCollisionEntered;
    public event UnityAction<HealingItem> MedicineChestCollisionEntered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            CoinCollisionEntered?.Invoke(coin);
        }

        if (other.TryGetComponent(out HealingItem healingItem))
        {
            MedicineChestCollisionEntered?.Invoke(healingItem);
        }
    }
}
