using System.Collections;
using UnityEngine;

namespace Enemies {
    public class Bat : MonoBehaviour {

        [SerializeField] private uint health;

        private Animator anim;
        private SpriteRenderer sprite;

        private void Start() {
            anim = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Damage")) {
                TakeDamage();
            }
        }

        public void TakeDamage() {
            health -= Controllers.LevelController.instance.playerDamage;

            if (health <= 0) {
                anim.Play("BatDeath");
            } else {
                StartCoroutine(FlashDamage());
            }
        }

        private IEnumerator FlashDamage() {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            sprite.color = Color.white;
        }
        
        // Called from animator
        public void RemoveEnemy() {
            Destroy(gameObject);
        }
    }
}