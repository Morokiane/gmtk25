using UnityEngine;

namespace Objects {
    public class Pot : MonoBehaviour {
        [SerializeField] private uint health;
        [Header("Drop Settings")]
        [SerializeField] private GameObject[] drops;
        [Tooltip("Percent Change of each item. Must add up to 100%")]
        [SerializeField] private float[] dropChance;

        private bool canBreak;
        
        private SpriteRenderer spriteRenderer;
        private Animator anim;

        private void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
        }
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                canBreak = true;
            } else if (other.CompareTag("Damage") && canBreak) {
                TakeDamage();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                canBreak = false;
            }
        }

        private void TakeDamage() {
            health -= Controllers.GameController.instance.playerDamage;

            if (health <= 0) {
                anim.Play("Pot");
                CalcDrop();
            }
        }

        private void CalcDrop() {
            float roll = Random.Range(0f, 100f);
            float cumulative = 0f;

            for (int i = 0; i < dropChance.Length; i++) {
                cumulative += dropChance[i];
                if (roll < cumulative) {
                    Instantiate(drops[i], transform.position, Quaternion.identity, transform.parent);
                    return;
                }
            }
        }

        public void RemoveObject() {
            Destroy(gameObject);
        }
	}
}
