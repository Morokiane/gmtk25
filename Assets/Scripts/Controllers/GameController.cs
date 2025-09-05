using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Controllers {
    public class GameController : MonoBehaviour {
        public static GameController instance;

        [Header("Coin stuff")]
        public uint coinsCollected; // coins collected in the level
        public uint coinsToOpen;    // coins required to open the master door
        public uint totalCoins;     // total coins collected thus far
        [SerializeField] private uint maxCoins; // Max coins the player can collect
        public uint chestKeys;
        [SerializeField] private uint maxChestKeys = 3;
        // Rooms are 0 - 7 this tells the game how to configure the exit depending on the current room number
        [HideInInspector] public int currentRoom;
        [Header("Available rooms to load")]
        public GameObject[] rooms;

        public uint loopLevel; // Each completed loop increases the level
        public GameObject currentRoomInstance;

        [Header("Player stuff")]
        public uint playerDamage;
        public bool playerDead;
        public float stunDuration;

        private int lastRoomIndex = -1;
        private PlayerInput playerInput;

        [HideInInspector] public bool masterDoorActive;
        public InputAction menuOpenCloseInput { get; private set; }

        private void Start() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }

            playerInput = GetComponent<PlayerInput>();
            menuOpenCloseInput = playerInput.actions["Submit"];
            // playerDamage = 1;
            // coinsToOpen = 5;
            // coinsCollected = 5;

            // Loads the master room (make sure it is always 0 in the array)
            currentRoomInstance = Instantiate(rooms[0], transform.position, Quaternion.identity);
        }

        public void ChangeRoom() {
            HUDController.instance.FadeIn();

            if (!playerDead) { 
                StartCoroutine(FadeIn());
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

        public void QuitGame() {
            Application.Quit();
        }
    }
}