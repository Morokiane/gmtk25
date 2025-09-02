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
            
            if (!GameController.instance.playerDead) {
                GameController.instance.loopLevel += 1;
                GameController.instance.totalCoins = GameController.instance.coinsCollected;
                GameController.instance.coinsCollected = 0;
                HUDController.instance.CalcCoins();
                HUDController.instance.UpdateLoop();
            } else {
                GameController.instance.playerDead = false;
            }
            
            Debug.Log("Master room loaded -" + " Loop level: " + GameController.instance.loopLevel);
        }

        public void OpenDoor() {
            anim.Play("DungeonDoorTop");
        }
    }
}
