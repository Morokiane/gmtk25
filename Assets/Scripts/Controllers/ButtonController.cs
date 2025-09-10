using UnityEngine;

namespace Controllers {
    public class ButtonController : MonoBehaviour {

        public void Heart() {
            if (Player.Player.instance.maxHealth < Player.Player.instance.health && GameController.instance.totalCoins >= GameController.instance.hpPotCost) {
                Player.Player.instance.health += 1;
            }
        }

        public void ChestKey() {
            if (GameController.instance.chestKeys < GameController.instance.maxChestKeys && GameController.instance.totalCoins >= GameController.instance.keyCost) {
                GameController.instance.chestKeys += 1;
                GameController.instance.totalCoins -= GameController.instance.keyCost;
                HUDController.instance.ShowTotalCoins();
                HUDController.instance.CalcKeys();
            }
        }
    }
}
