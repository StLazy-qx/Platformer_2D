using UnityEngine;

public class MedicineChest : MonoBehaviour
{
    [SerializeField] private int _healValue = 20;

    public int HealValue => _healValue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Health targetHealth = player.GetComponent<Health>();
            
            if (targetHealth != null)
            {
                targetHealth.TakeHeal(_healValue);
            }

            Destroy(gameObject);
        }
    }
}
