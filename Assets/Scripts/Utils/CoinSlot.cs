using UnityEngine;
using Controllers;

namespace Utils {
    public class CoinSlot : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                HUDController.instance.ShowCostWindow();
                GameController.instance.masterDoorActive = true;
            }
            // if (other.CompareTag("Player")) {
            //     if (Controllers.GameController.instance.coinsCollected >= Controllers.GameController.instance.coinsToOpen) {
            //         anim.Play("DungeonDoorTop");
            //     }
            // }  
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                HUDController.instance.ShowCostWindow();
                GameController.instance.masterDoorActive = false;
            }
        }
    }
}
