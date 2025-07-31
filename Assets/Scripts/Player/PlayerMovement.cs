using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerMovement : MonoBehaviour {
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int LastInputX = Animator.StringToHash("LastInputX");
        private static readonly int LastInputY = Animator.StringToHash("LastInputY");
        private static readonly int InputX = Animator.StringToHash("InputX");
        private static readonly int InputY = Animator.StringToHash("InputY");
        
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private Vector2 moveInput;
        [Header("Components")]
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Animator anim;
        [SerializeField] private BoxCollider2D boxCollider2D;
        // [SerializeField] private CircleCollider2D circleCollider2D;
        private Vector2 lastDirection;

        private void FixedUpdate() {
            rb.linearVelocity = moveInput * moveSpeed;
        }
        
        public void Move(InputAction.CallbackContext context) {
            moveInput = context.ReadValue<Vector2>();

            anim.SetFloat(InputX, moveInput.x);
            anim.SetFloat(InputY, moveInput.y);

            bool isMoving = moveInput != Vector2.zero;
            anim.SetBool(IsWalking, isMoving);

            // Only update lastDirection when actually moving
            if (isMoving) {
                lastDirection = moveInput.normalized;
                anim.SetFloat(LastInputX, lastDirection.x);
                anim.SetFloat(LastInputY, lastDirection.y);
            }
        }


        // Invoked from the PlayerInput component
        /*public void Move(InputAction.CallbackContext context) {
            // if (Player.instance.canMove) {
                anim.SetBool(IsWalking, true);

                if (context.canceled) {
                    anim.SetBool(IsWalking, false);
                    anim.SetFloat(LastInputX, moveInput.x);
                    anim.SetFloat(LastInputY, moveInput.y);
                }

                // Debug.Log("X " + moveInput.x + " " + "Y " + moveInput.y);
                // if (moveInput != lastDirection) {
                //     if (moveInput.y > 0) {
                //         LevelController.instance.facingUp = true;
                //         LevelController.instance.facingDown = false;
                //     } else if (moveInput.y < 0) {
                //         LevelController.instance.facingDown = true;
                //         LevelController.instance.facingUp = false;
                //     } else if (moveInput.x != 0) {
                //         // Moving left or right — not up or down
                //         LevelController.instance.facingUp = false;
                //         LevelController.instance.facingDown = false;
                //     }
                // }

                // Debug.Log(LevelController.instance.facingUp + " " + LevelController.instance.facingDown);
                
                moveInput = context.ReadValue<Vector2>();
                anim.SetFloat(InputX, moveInput.x);
                anim.SetFloat(InputY, moveInput.y);
            // }

            // if (Player.instance.canMove) {
                anim.SetBool(IsWalking, false);
                anim.SetFloat(LastInputX, moveInput.x);
                anim.SetFloat(LastInputY, moveInput.y);
            // }
        }*/
    }
}
