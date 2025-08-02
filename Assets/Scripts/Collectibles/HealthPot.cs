using UnityEngine;

namespace Collectibles {
    public class HealthPot : MonoBehaviour {

        [SerializeField] private int healAmount;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player") && Player.Player.instance.health < Player.Player.instance.maxHealth) {
                Player.Player.instance.health += healAmount;
                Controllers.HUDController.instance.UpdateHUD(Player.Player.instance.health);
                Destroy(gameObject);
            }
        }
    }
}