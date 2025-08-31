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
            LevelController.instance.coinsCollected = LevelController.instance.totalCoins;
            
            if (!LevelController.instance.playerDead) {
                LevelController.instance.loopLevel += 1;
            } else {
                LevelController.instance.playerDead = false;
            }
            
            Debug.Log("Master room loaded -" + " Loop level: " + LevelController.instance.loopLevel);
        }

        public void OpenDoor() {
            anim.Play("DungeonDoorTop");
        }
    }
}
