using UnityEngine;

namespace Utils {
    public class PursueMover : MonoBehaviour {
        [Header("Movement speeds")] 
        public float moveSpeed = 5;
        public bool delayPursue;
        public float delayTime = 2;
        public float prePursueTime = 1;
        public float initialRotation;
        public float rotationSpeed = 3.5f;
        [SerializeField] private bool destroyObject;

        private bool isPursuing;
        private float delayTimer;
        private Vector2 offScreenPosition;

        private void Start() {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, initialRotation)); // ok if you want a fixed starting rotation
            rotationSpeed *= 100;

            if (delayPursue) {
                delayTimer = delayTime;
            } else {
                isPursuing = true;
            }
        }

        private void FixedUpdate() {
            if (!Player.Player.instance.takeDamage && isPursuing) {
                isPursuing = false;
                LostPlayer();
            }

            if (delayPursue) {
                delayTimer -= Time.deltaTime;
                if (delayTimer <= 0) {
                    isPursuing = true;
                }
            }

            if (!isPursuing) {
                if (delayTimer <= prePursueTime) {
                    // No need to rotate
                }

                // Move in direction of player without rotating
                Vector2 direction = (Player.Player.instance.transform.position - transform.position).normalized;
                transform.position += (Vector3)(direction * (moveSpeed * Time.deltaTime));
                
                if (direction.x > 0) {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                } else if (direction.x < 0) {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            } else {
                PursuePlayer();
            }
        }

        private void PursuePlayer() {
            Vector2 playerPos = Player.Player.instance.transform.position;
            Vector2 objectPos = transform.position;
            
            if (Player.Player.instance.takeDamage) {
                transform.position = Vector2.MoveTowards(objectPos, playerPos, moveSpeed * Time.fixedDeltaTime);

                // Removed rotation
                // RotateTowardsPlayer(); 
            }
        }

        // You can delete this whole method if you're not using rotation anymore
        private void RotateTowardsPlayer() {
            // no longer needed
        }

        private void LostPlayer() {
            Vector2 objectPos = transform.position;
            offScreenPosition = new Vector2(-Screen.width + 50f, objectPos.y);

            transform.position = Vector2.MoveTowards(objectPos, offScreenPosition, moveSpeed * Time.fixedDeltaTime);

            // Removed rotation
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            if (gameObject.transform.position.x < -13 && destroyObject) {
                Destroy(gameObject);
            } else if (gameObject.transform.position.x < -13 && !destroyObject) {
                gameObject.SetActive(false);
            }
        }
    }
}
