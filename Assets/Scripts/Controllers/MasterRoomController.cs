using UnityEngine;

namespace Controllers {
    public class MasterRoomController : MonoBehaviour {
        public static MasterRoomController instance;

        [SerializeField] private GameObject masterDoor;

        private Animator anim;

        private void Start() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }

            anim = masterDoor.GetComponent<Animator>();
            // LevelController.instance.coinsCollected = LevelController.instance.totalCoins;
            LevelController.instance.loopLevel += 1;
            Debug.Log("Master room loaded -" + " Loop level: " + LevelController.instance.loopLevel);
        }

        public void OpenDoor() {
            anim.Play("DungeonDoorTop");
        }
    }
}
