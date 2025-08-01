using UnityEngine;

namespace Utils {
    public class Exit : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                Player.Player.instance.canExit = true;
                Debug.Log(Player.Player.instance.canExit);
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                Player.Player.instance.canExit = false;
                Debug.Log(Player.Player.instance.canExit);
            }
        }
    }
}
