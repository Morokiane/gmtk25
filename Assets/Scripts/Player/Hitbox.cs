using UnityEngine;

namespace Player {
    public class Hitbox : MonoBehaviour {
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Enemy")) {
                Player.instance.DamagePlayer(1);
            }
        }
    }
}