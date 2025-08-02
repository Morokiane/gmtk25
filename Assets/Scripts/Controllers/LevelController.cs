using System.Collections;
using UnityEngine;

namespace Controllers {
	public class LevelController : MonoBehaviour {
		public static LevelController instance;
		[SerializeField] private uint coinsCollected;
		public int currentRoom = 0;
		[Header("Available rooms to load")]
		public GameObject[] rooms;
		
		private uint loopLevel; // Each completed loop increases the level
		public GameObject currentRoomInstance;

		private void Start() {

			if (instance == null) {
				instance = this;
			} else {
				Destroy(gameObject);
			}

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
			currentRoomInstance = Instantiate(rooms[1], transform.position, Quaternion.identity);
		}

		private IEnumerator FadeOut() {
			yield return new WaitForSeconds(2f);
			HUDController.instance.FadeOut();
			Player.Player.instance.canMove = true;
		}
	}
}