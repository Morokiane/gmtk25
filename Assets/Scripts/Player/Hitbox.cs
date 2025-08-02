using UnityEngine;

namespace Player {
    public class Hitbox : MonoBehaviour {
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Enemy")) {
                Debug.Log("hit by enemy");
                Player.instance.health--;
                Controllers.HUDController.instance.UpdateHUD(Player.instance.health);
                StartCoroutine(Controllers.HUDController.instance.Shake(0.4f, 0.15f));
            }
        }
    }
}