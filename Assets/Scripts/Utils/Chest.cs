using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils {
    public class Chest : MonoBehaviour {
        public static Chest instance;

        // [SerializeField] private Spawner spawner;
        [SerializeField] private Sprite chestOpenSprite;

        private bool chestOpen;
        private SpriteRenderer spriteRenderer;
        
        private void Start() {
            instance = this;
            spriteRenderer = GetComponent<SpriteRenderer>();
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
            if (!chestOpen) {
                chestOpen = true;
                int randCoins = Random.Range(1, 5);
                // spawner.Spawn(3);
                LevelController.instance.coins += randCoins;
                HUDController.instance.CalcCoins();
                spriteRenderer.sprite = chestOpenSprite;
            }
            // Instantiate(chestLoot, transform.position, Quaternion.identity);
        }
    }
}