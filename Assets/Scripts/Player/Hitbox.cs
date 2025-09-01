using UnityEngine;

namespace Player {
    public class Hitbox : MonoBehaviour {
        [SerializeField] private float tickRate = 1f;

        private int contacts = 0;
        private float timer = 0;

        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.CompareTag("Enemy")) return;
            contacts++;
            if (contacts == 1) {
                timer = 0f;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (!other.CompareTag("Enemy")) return;

            contacts = Mathf.Max(0, contacts - 1);
        }

        private void Update() {
            if (contacts <= 0) return;

            timer -= Time.deltaTime;
            if (timer <= 0) {
                Player.instance.DamagePlayer(Enemies.Enemy.instance.damageToPlayer);
                timer = tickRate;
            }
        }
    }
}