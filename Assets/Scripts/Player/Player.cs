using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class Player : MonoBehaviour {
        private static readonly int AttackTrigger = Animator.StringToHash("isAttacking");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        public static Player instance;

        private Animator anim;
        private PlayerMovement playerMovement;
        private Rigidbody2D rb;

        public int health;
        public bool canMove = true;
        public bool isAttacking;
        public bool canExit;

        private void Start() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }

            rb = GetComponent<Rigidbody2D>();
            playerMovement = GetComponent<PlayerMovement>();
            anim = GetComponent<Animator>();
            
            // isAttacking = false;
        }
        
        public void OnAttack(InputAction.CallbackContext context) {
            if (context.performed) {
                isAttacking = true;
                canMove = false;
                rb.linearVelocity = Vector2.zero;
                playerMovement.moveInput = Vector2.zero;
                anim.SetBool(IsWalking, false);
                anim.SetTrigger(AttackTrigger);
            }
        }
                
        public void OnInteract(InputAction.CallbackContext context) {
            // context.started gets only when the key is pressed
            if (context.started && canExit) {
                Debug.Log("loading level");
                Controllers.LevelController.instance.ChangeRoom();
            }
        }

        public void ResetAttack() {
            canMove = true;
            isAttacking = false;
            anim.SetBool(AttackTrigger, false);
        }
    }
}
