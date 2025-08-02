using Controllers;
using UnityEngine;

namespace Utils {
    public class ResetRoomNum : MonoBehaviour {

        public void ResetRoom() {
            if (LevelController.instance.currentRoom > 8) {
                LevelController.instance.currentRoom = 0;
                Debug.Log("resetting current room");
            }
        }
    }
}