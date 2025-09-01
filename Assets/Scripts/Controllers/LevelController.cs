using UnityEngine;
using System.Collections;

namespace Controllers {
    public class LevelController : MonoBehaviour {
        public static LevelController instance;

        [Header("Coin stuff")]
        public uint coinsCollected; // coins collected in the level
        public uint coinsToOpen; //coins required to open the master door
        public uint totalCoins; //total coins collected thus far
        // Rooms are 0 - 7 this tells the game how to configure the exit depending on the current room number
        [HideInInspector] public int currentRoom;
        [Header("Available rooms to load")]
        public GameObject[] rooms;

        public uint loopLevel; // Each completed loop increases the level
        public GameObject currentRoomInstance;

        [Header("Player stuff")]
        public uint playerDamage;
        public bool playerDead;

        private int lastRoomIndex = -1;

        [HideInInspector] public bool masterDoorActive;

        private void Start() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
    
            // playerDamage = 1;
            // coinsToOpen = 5;
            // coinsCollected = 5;

            // Loads the master room
            currentRoomInstance = Instantiate(rooms[0], transform.position, Quaternion.identity);
        }

        public void ChangeRoom() {
            HUDController.instance.FadeIn();

            if (!playerDead) { 
                StartCoroutine(FadeIn()); // Think I should change the name of this
                StartCoroutine(FadeOut());
            } else {
                StartCoroutine(PlayerDied());
            }
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
            // Debug.Log(currentRoom + " room loaded");
        }

        private IEnumerator FadeOut() {
            yield return new WaitForSeconds(2f);
            HUDController.instance.FadeOut();
            Player.Player.instance.canMove = true;
        }

        private IEnumerator PlayerDied() {
            yield return new WaitForSecondsRealtime(1f);

            Player.Player.instance.transform.position = new Vector2(0f, 0f);
            Destroy(currentRoomInstance);
            currentRoomInstance = Instantiate(rooms[0], transform.position, Quaternion.identity);
            // Debug.Log(currentRoomInstance);
            currentRoom = 0;
            Player.Player.instance.Reset();
            StartCoroutine(FadeOut());
        }
    }
}