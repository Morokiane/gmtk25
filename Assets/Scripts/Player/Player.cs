using UnityEngine;

namespace Player {
    public class Player : MonoBehaviour {
        public static Player instance;

        public bool canMove = true;

        private void Start() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
        }
    }
}
