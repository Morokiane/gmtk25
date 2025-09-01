using System.Collections;
using UnityEngine;
using Utils;

namespace Enemies {
    public class Enemy : MonoBehaviour {
        public static Enemy instance;

        public uint health;
        public uint maxHealth;
        [Header("Drop Settings")]
        [SerializeField] private GameObject[] drops;
        [Tooltip("Percent Change of each item. Must add up to 100%")]
        [SerializeField] private float[] dropChance;
        
        [Header("Damage Variables")]
        public int damageToPlayer = 1;
        // This is set by the room controller
        [HideInInspector] public bool needToCount;
        
        private Animator anim;
        private SpriteRenderer sprite;
        private CircleCollider2D circleCollider2D;
        private PursueMover pursueMover;
        
        private void Start() {
            instance = this;
            
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
            circleCollider2D = GetComponent<CircleCollider2D>();
            pursueMover = GetComponent<PursueMover>();

            // This will cause an error in the master room
            if (Controllers.RoomController.instance.goal == 0) {
                needToCount = true;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Damage")) {
                TakeDamage();
                // pursueMover.ApplyKnockback(transform.position);
            }
        }

        private void TakeDamage() {
            health -= Controllers.LevelController.instance.playerDamage;

            if (health <= 0) {
                circleCollider2D.enabled = false;
                anim.SetTrigger("Death");
                CalcDrop();
            } else {
                StartCoroutine(FlashDamage());
            }
        }
        
        private IEnumerator FlashDamage() {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
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
        
        // Called from animator
        public void RemoveEnemy() {
            if (needToCount) {
                Controllers.RoomController.instance.RecalcEnemy();
            }

            Destroy(gameObject);
        }
    }
}