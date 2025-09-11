using UnityEngine;

namespace Controllers {
    public class ButtonController : MonoBehaviour {

        public void HalfHeart() {
            if (Player.Player.instance.health < Player.Player.instance.maxHealth && GameController.instance.totalCoins >= GameController.instance.halfHeartCost) {
                Player.Player.instance.health += 1;
                GameController.instance.totalCoins -= GameController.instance.halfHeartCost;
                HUDController.instance.UpdateHUD(Player.Player.instance.health);
            }
        }

        public void FullHeart() {
            if (Player.Player.instance.health < (Player.Player.instance.maxHealth - 1) && GameController.instance.totalCoins >= GameController.instance.fullHeartCost) {
                Player.Player.instance.health += 2;
                GameController.instance.totalCoins -= GameController.instance.fullHeartCost;
                HUDController.instance.UpdateHUD(Player.Player.instance.health);
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
