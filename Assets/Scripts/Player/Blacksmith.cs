using UnityEngine;
using Controllers;

namespace Player {
    public class Blacksmith : MonoBehaviour {
        [SerializeField] private GameObject speechExclaim;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                speechExclaim.SetActive(true);
                MasterRoomController.instance.blacksmithInteract = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                speechExclaim.SetActive(false);
                MasterRoomController.instance.blacksmithInteract = false;
            }
        }
    }
}
