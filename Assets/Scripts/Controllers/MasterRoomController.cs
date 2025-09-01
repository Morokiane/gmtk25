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
            
            if (!LevelController.instance.playerDead) {
                LevelController.instance.loopLevel += 1;
                LevelController.instance.totalCoins = LevelController.instance.coinsCollected;
                LevelController.instance.coinsCollected = 0;
                HUDController.instance.CalcCoins();
                HUDController.instance.UpdateLoop();
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
