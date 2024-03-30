using UnityEngine;
using UnityEngine.Events;

public class CharacterGlobalCollisionHandler : MonoBehaviour
{
    public event UnityAction<Coin> OnCoinCollisionEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            OnCoinCollisionEnter?.Invoke(coin);
        }
    }
}
