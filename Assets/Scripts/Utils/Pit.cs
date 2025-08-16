using UnityEngine;

namespace Utils {
    public class Pit : MonoBehaviour {
        [SerializeField] private int damage;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                Player.Player.instance.FallIntoPit();
                Player.Player.instance.DamagePlayer(damage);
            }
        }
    }
}