using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Objects {
    public class Chest : MonoBehaviour {
        public static Chest instance;

        [SerializeField] private GameObject[] loot;
        [SerializeField] private uint lootMin;
        [SerializeField] private uint lootMax;
        [SerializeField] private float[] dropChance;
        [Header("Chest graphics")]
        [SerializeField] private Sprite[] chestClosed;
        [SerializeField] private Sprite[] chestOpen;

        private bool chestOpened;
        private int newSprite;
        
        private SpriteRenderer spriteRenderer;
        
        private void Start() {
            instance = this;
            spriteRenderer = GetComponent<SpriteRenderer>();

            SetGraphic();
        }

        private void SetGraphic() {
            newSprite = Random.Range(0, chestClosed.Length);
            spriteRenderer.sprite = chestClosed[newSprite];
            // Can add a feature that checks which chest and adjust drop chances depending on the chest
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
            if (!chestOpened && GameController.instance.chestKeys > 0) {
                chestOpened = true;
                spriteRenderer.sprite = chestOpen[newSprite];
                GameController.instance.chestKeys--;
                HUDController.instance.CalcKeys();
                SpawnLoot();
            }
        }

        private void SpawnLoot() {
            var lootCount = Random.Range(lootMin, lootMax + 1);

            for (int i = 0; i < lootCount; i++) {
                GameObject prefab = CalcDrop();
                if (prefab == null) continue;

                GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
            }
        }

        private GameObject CalcDrop() {
            float roll = Random.Range(0f, 100f);
            float cumulative = 0f;

            for (int i = 0; i < dropChance.Length; i++) {
                cumulative += dropChance[i];
                if (roll < cumulative) {
                    return loot[i];
                }
            }

            return null; // in case nothing matched (e.g., dropChance doesn’t sum to 100)
        }
    }
}