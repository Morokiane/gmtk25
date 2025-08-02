using System.Collections;
using UnityEngine;

namespace Controllers {
	public class LevelController : MonoBehaviour {
		public static LevelController instance;
		[SerializeField] private uint coinsCollected;
		public int currentRoom;
		[Header("Available rooms to load")]
		public GameObject[] rooms;
		
		private uint loopLevel; // Each completed loop increases the level
		public GameObject currentRoomInstance;

        public int coins;
        public uint playerDamage;
        public int amountOfEnemies;
        
        private int lastRoomIndex = -1; // Declare this somewhere in your class

		private void Start() {
			if (instance == null) {
				instance = this;
			} else {
				Destroy(gameObject);
			}
            
            playerDamage = 1;

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