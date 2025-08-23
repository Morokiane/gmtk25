using UnityEngine;

namespace Utils {
    public class CoinSlot : MonoBehaviour {
        [SerializeField] private GameObject masterDoor;
        private Animator anim;

        private void Start() {
            anim = masterDoor.GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                if (Controllers.LevelController.instance.coinsCollected >= 5) {
                    anim.Play("DungeonDoorTop");
                }
            }  
        }
    }
}
