using System.Collections;
using UnityEngine;

namespace Enemies {
    public class Bat : MonoBehaviour {

        [SerializeField] private uint health;
        [SerializeField] private GameObject[] drops;

        private Animator anim;
        private SpriteRenderer sprite;
        private Utils.PursueMover pursueMover;

        private void Start() {
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
            pursueMover = GetComponent<Utils.PursueMover>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Damage")) {
                TakeDamage();
                pursueMover.ApplyKnockback(transform.position);
            }
        }

        private void TakeDamage() {
            health -= Controllers.LevelController.instance.playerDamage;

            if (health <= 0) {
                anim.Play("BatDeath");
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
            float dropChance = 30f;
            float roll = Random.Range(0f, 100f);

            if (roll < dropChance) {
                Instantiate(drops[1], transform.position, Quaternion.identity, transform.parent);
            } else {
                Instantiate(drops[0], transform.position, Quaternion.identity, transform.parent);
            }
        }
        
        // Called from animator
        public void RemoveEnemy() {
            Destroy(gameObject);
        }
    }
}