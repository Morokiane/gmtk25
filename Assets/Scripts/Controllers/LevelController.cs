using System.Collections;
using UnityEngine;

namespace Controllers {
    public class LevelController : MonoBehaviour {
        public static LevelController instance;

        public uint coinsCollected;
        public uint coinsToOpen;
        // Rooms are 0 - 7 this tells the game how to configure the exit depending on the current room number
        [HideInInspector] public int currentRoom;
        [Header("Available rooms to load")]
        public GameObject[] rooms;

        public uint loopLevel; // Each completed loop increases the level
        public GameObject currentRoomInstance;

        public uint totalCoins; // Keeping this as maybe a sort of score
        public uint playerDamage;

        private int lastRoomIndex = -1;

        [HideInInspector] public bool masterDoorCoinSlot;

        private void Start() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
    
            playerDamage = 1;
            // coinsToOpen = 5;
            coinsCollected = 15;

            Debug.Log(coinsCollected);

            currentRoomInstance = Instantiate(rooms[0], transform.position, Quaternion.identity);
        }

        public void ChangeRoom() {
            HUDController.instance.FadeIn();
            StartCoroutine(FadeIn());
            StartCoroutine(FadeOut());
        }

        // This really should be called fade out...fucked that up
        private IEnumerator FadeIn() {
            yield return new WaitForSecondsRealtime(1f);
            Player.Player.instance.canMove = false;

            Destroy(currentRoomInstance);

            currentRoom++;

            // Pick a room that’s not the same as last time
            int roomToSpawn;
            do {
                roomToSpawn = Random.Range(1, rooms.Length);
            } while (roomToSpawn == lastRoomIndex && rooms.Length > 2);

            lastRoomIndex = roomToSpawn;

            currentRoomInstance = Instantiate(rooms[roomToSpawn], transform.position, Quaternion.identity);
        }

        private IEnumerator FadeOut() {
            yield return new WaitForSeconds(2f);
            HUDController.instance.FadeOut();
            Player.Player.instance.canMove = true;
        }
    }
}