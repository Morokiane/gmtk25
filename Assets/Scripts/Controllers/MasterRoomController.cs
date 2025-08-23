using UnityEngine;

namespace Controllers {
    public class MasterRoomController : MonoBehaviour {

        private void Start() {
            Controllers.LevelController.instance.coinsCollected = Controllers.LevelController.instance.coins;
        }
    }
}
