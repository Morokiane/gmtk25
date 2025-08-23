using UnityEngine;

namespace Objects {
    public class Pot : MonoBehaviour {
        [SerializeField] private uint health;
        [Header("Drop Settings")]
        [SerializeField] private GameObject[] drops;
        [Tooltip("Percent Change of each item. Must add up to 100%")]
        [SerializeField] private float[] dropChance;

        private bool canBreak;
        private bool broken;
        
        private SpriteRenderer spriteRenderer;
        private CapsuleCollider2D capsuleCollider2D;
        private Animator anim;

        private void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            anim = GetComponent<Animator>();
        }
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player") && !broken) {
                canBreak = true;
            } else if (other.CompareTag("Damage") && !broken) {
                TakeDamage();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player") && !broken) {
                canBreak = false;
            }
        }

        private void TakeDamage() {
            health -= Controllers.LevelController.instance.playerDamage;

            if (health <= 0) {
                capsuleCollider2D.enabled = false;
                anim.Play("Pot");
                broken = true;
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
