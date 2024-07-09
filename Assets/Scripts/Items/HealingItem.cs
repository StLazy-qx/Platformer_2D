using UnityEngine;

public class HealingItem : MonoBehaviour
{
    [SerializeField] private int _healAmount = 20;

    public int HealAmount => _healAmount;
}
