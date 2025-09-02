using Controllers;
using UnityEngine;

namespace Utils {
    public class ResetRoomNum : MonoBehaviour {

        public void ResetRoom() {
            if (GameController.instance.currentRoom > 8) {
                GameController.instance.currentRoom = 0;
                Debug.Log("resetting current room");
            }
        }
    }
}