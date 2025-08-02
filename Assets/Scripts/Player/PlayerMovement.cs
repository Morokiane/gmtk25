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
        [SerializeField] public Vector2 moveInput;
        [SerializeField] private Transform frontMarker;
        [SerializeField] private float frontDistance = 0.5f;
        
        private Rigidbody2D rb;
        private Animator anim;
        private BoxCollider2D boxCollider2D;
        private PlayerInput playerInput;
        
        private Vector2 lastDirection;
        private InputAction moveAction;
        
        private void Awake() {
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
        }

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate() {
            if (Player.instance.canMove) {
                rb.linearVelocity = moveInput * moveSpeed;
            } else {
                rb.linearVelocity = Vector2.zero;
            }
        }
        
        private void Update() {
            if (Player.instance.canMove && !Player.instance.isAttacking) {
                Vector2 input = moveAction.ReadValue<Vector2>();

                if (input != moveInput) {
                    moveInput = input;
                    anim.SetFloat(InputX, input.x);
                    anim.SetFloat(InputY, input.y);

                    bool isMoving = input != Vector2.zero;
                    anim.SetBool(IsWalking, isMoving);

                    if (isMoving) {
                        lastDirection = input.normalized;
                        anim.SetFloat(LastInputX, lastDirection.x);
                        anim.SetFloat(LastInputY, lastDirection.y);
                    }
                }
            }

            if (frontMarker != null) {
                frontMarker.localPosition = lastDirection * frontDistance;
            }
        }
        
        // public void Move(InputAction.CallbackContext context) {
        //     if (!Player.instance.canMove || Player.instance.isAttacking) return;
        //     
        //     moveInput = context.ReadValue<Vector2>();
        //
        //     anim.SetFloat(InputX, moveInput.x);
        //     anim.SetFloat(InputY, moveInput.y);
        //
        //     bool isMoving = moveInput != Vector2.zero;
        //     anim.SetBool(IsWalking, isMoving);
        //
        //     // Only update lastDirection when actually moving
        //     if (isMoving) {
        //         lastDirection = moveInput.normalized;
        //         anim.SetFloat(LastInputX, lastDirection.x);
        //         anim.SetFloat(LastInputY, lastDirection.y);
        //     }
        //
        //     if (Player.instance.isAttacking) {
        //         anim.SetBool(IsWalking, false);
        //     }
        // }
    }
}
