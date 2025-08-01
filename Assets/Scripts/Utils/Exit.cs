using UnityEngine;

namespace Utils {
    public class Exit : MonoBehaviour {

        // Load the next level when player enters
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                Controllers.LevelController.instance.ChangeRoom();
                // Player.Player.instance.canExit = true;
            }
        }

        // I shouldn't need this any longer
        /* private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                Player.Player.instance.canExit = false;
                Debug.Log(Player.Player.instance.canExit);
            }
        } */
    }
}
