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
        
        private bool isKnockedBack;
        private float knockbackTimer;
        
        [Header("Knockback Settings")]
        [SerializeField] private float knockbackDuration = 0.2f;
        [SerializeField] private float knockbackForce = 5f;

        private Rigidbody2D rb;

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
            // Don't think I need rotation stuff, but I'm going to keep it just in case
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, initialRotation)); // ok if you want a fixed starting rotation
            rotationSpeed *= 100;

            if (delayPursue) {
                delayTimer = delayTime;
            } else {
                isPursuing = true;
            }
        }

        private void FixedUpdate() {
            if (isKnockedBack) {
                knockbackTimer -= Time.deltaTime;
                if (knockbackTimer <= 0f) {
                    isKnockedBack = false;
                    rb.linearVelocity = Vector2.zero;
                }
                return; // Skip normal movement during knockback
            }
            
            if (delayPursue) {
                delayTimer -= Time.deltaTime;

                if (delayTimer <= 0) {
                    delayPursue = false;
                    isPursuing = true;
                }
            }

            if (!delayPursue && isPursuing && !Player.Player.instance.takeDamage) {
                PursuePlayer();
            }

            // This changes the enemy to face the player
            Vector2 direction = (Player.Player.instance.transform.position - transform.position).normalized;

            if (direction.x > 0) {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else if (direction.x < 0) {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.lossyScale.z);
            }
        }

        private void PursuePlayer() {
            Debug.Log("pursuing player");
            Vector2 playerPos = Player.Player.instance.transform.position;
            Vector2 objectPos = transform.position;
            
            if (!Player.Player.instance.takeDamage) {
                transform.position = Vector2.MoveTowards(objectPos, playerPos, moveSpeed * Time.fixedDeltaTime);
            }
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
        
        public void ApplyKnockback(Vector2 sourcePosition) {
            isKnockedBack = true;
            knockbackTimer = knockbackDuration;

            Vector2 direction = (transform.position - (Vector3)sourcePosition).normalized;
            rb.linearVelocity = direction * knockbackForce;
        }
    }
}
