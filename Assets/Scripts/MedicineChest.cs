using UnityEngine;

public class MedicineChest : MonoBehaviour
{
    [SerializeField] private int _heal = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            Destroy(gameObject);
    }
}
