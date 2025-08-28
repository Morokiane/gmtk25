using UnityEngine;

namespace Controllers {
    public class MasterRoomController : MonoBehaviour {
        public static MasterRoomController instance;

        [SerializeField] private GameObject masterDoor;

        private Animator anim;

        private void Start() {
            anim = masterDoor.GetComponent<Animator>();
            Debug.Log(anim);
            // LevelController.instance.coinsCollected = LevelController.instance.totalCoins;
            LevelController.instance.loopLevel += 1;
            Debug.Log("Master room loaded" + " Loop level: " + LevelController.instance.loopLevel);
        }

        public void OpenDoor() {
            Debug.Log("open the fucking door");
            anim.Play("DungeonDoorTop");
        }
    }
}
