using UnityEngine;
using Controllers;

namespace Utils {
    public class CoinSlot : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                HUDController.instance.ShowCostWindow();
                LevelController.instance.masterDoorCoinSlot = true;
            }
            // if (other.CompareTag("Player")) {
            //     if (Controllers.LevelController.instance.coinsCollected >= Controllers.LevelController.instance.coinsToOpen) {
            //         anim.Play("DungeonDoorTop");
            //     }
            // }  
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                HUDController.instance.ShowCostWindow();
                LevelController.instance.masterDoorCoinSlot = false;
            }
        }
    }
}
