using Controllers;
using UnityEngine;

namespace Collectibles {
    public class Coin : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                LevelController.instance.coinsCollected += 1;
                HUDController.instance.CalcCoins();
                Destroy(gameObject);
            }
        }
    }
}