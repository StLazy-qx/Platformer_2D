using UnityEngine;
using TMPro;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.MoneyChanged += OnCoinView;
    }

    private void OnDisable()
    {
        _wallet.MoneyChanged -= OnCoinView;
    }

    private void OnCoinView(int value)
    {
        _money.text = value.ToString();
    }
}
