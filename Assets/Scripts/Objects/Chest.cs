using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects {
    public class Chest : MonoBehaviour {
        public static Chest instance;

        // [SerializeField] private Spawner spawner;

        private bool chestOpen;
        private Animator anim;
        
        private void Start() {
            instance = this;
            anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                Player.Player.instance.canLoot = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                Player.Player.instance.canLoot = false;
            }
        }

        public void OpenChest() {
            if (!chestOpen && GameController.instance.chestKeys > 0) {
                chestOpen = true;
                int randCoins = Random.Range(3, 6);
                // spawner.Spawn(3);
                GameController.instance.coinsCollected += (uint)randCoins;
                HUDController.instance.CalcCoins();
                anim.Play("Chest");
            }
            // Instantiate(chestLoot, transform.position, Quaternion.identity);
        }
    }
}