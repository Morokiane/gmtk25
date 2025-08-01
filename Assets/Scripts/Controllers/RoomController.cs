using UnityEngine;

namespace Controllers {
    public class RoomController : MonoBehaviour {
        [SerializeField] private GameObject[] exits;
        
        private void Start() {
            ConfigureRoom();
        }

        private void ConfigureRoom() {
            Debug.Log("Current room number: " + LevelController.instance.currentRoom);

            switch (LevelController.instance.currentRoom) {
                case 1:
                    exits[0].SetActive(true);
                    break;
            }
        }
    }
}
