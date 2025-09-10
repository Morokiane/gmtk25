using UnityEngine;
using TMPro;
using Controllers;

namespace Utils {
    public class BSWindow : MonoBehaviour {
        [Header("Items costs")]
        [SerializeField] private TextMeshProUGUI hpPotCostText;
        [SerializeField] private TextMeshProUGUI keyCostText;

        private void OnEnable() {
            // This makes sure the game doesn't crash if the blacksmith menu is active
            if (GameController.instance == null) return;

            hpPotCostText.text = GameController.instance.hpPotCost.ToString();
            keyCostText.text = GameController.instance.keyCost.ToString();
        }
    }
}
