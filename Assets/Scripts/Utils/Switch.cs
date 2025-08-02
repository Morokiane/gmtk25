using UnityEngine;

namespace Utils {
    public class Switch : MonoBehaviour {
        public static Switch instance;
        
        [SerializeField] private Sprite switchDown;

        private SpriteRenderer sprite;
        // private CircleCollider2D circleCollider2D;

        private void Start() {
            instance = this;

            sprite = GetComponent<SpriteRenderer>();
            // circleCollider2D = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                Player.Player.instance.canInteract = true;
                Debug.Log(Player.Player.instance.canInteract);
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                Player.Player.instance.canInteract = false;
            }
        }

        public void FlipSwitch() {
            sprite.sprite = switchDown;
        }
    }
}