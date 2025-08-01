using System;
using System.Collections;
using UnityEngine;

namespace Controllers {
	public class LevelController : MonoBehaviour {
		public static LevelController instance;
		[SerializeField] private uint coinsCollected;
		public uint currentRoom = 0;
		[Header("Available rooms to load")]
		[SerializeField] private GameObject[] rooms;
		
		private uint loopLevel; // Each completed loop increases the level
		
		private void Start() {
			if (instance == null) {
				instance = this;
			} else {
				Destroy(gameObject);
			}
		}

		public void ChangeRoom() {
			HUDController.instance.FadeIn();
			StartCoroutine(FadeIn());
			StartCoroutine(FadeOut());
		}

		private IEnumerator FadeIn() {
			yield return new WaitForSecondsRealtime(1f);
			rooms[currentRoom].SetActive(false);
			currentRoom++;
			Instantiate(rooms[1], transform.position, Quaternion.identity);
		}

		private IEnumerator FadeOut() {
			yield return new WaitForSeconds(3f);
			HUDController.instance.FadeOut();
		}
	}
}