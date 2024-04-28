using TMPro;
using UnityEngine;

namespace _Project.Core.Items.UI
{
    [DisallowMultipleComponent]
    public sealed class CoinsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsText;
        
        public void UpdateText(in int coins)
        {
            _coinsText.SetText(coins.ToString());
        }
    }
}