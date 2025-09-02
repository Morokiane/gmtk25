using Controllers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class Player : MonoBehaviour {
        private static readonly int AttackTrigger = Animator.StringToHash("isAttacking");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        public static Player instance;

        [SerializeField] private GameObject hitbox;

        public int maxHealth;
        public int health;

        [HideInInspector] public bool canMove = true;
        [HideInInspector] public bool isAttacking;
        [HideInInspector] public bool canLoot;
        [HideInInspector] public bool takeDamage;
        [HideInInspector] public bool canInteract;
        
        private Animator anim;
        private PlayerMovement playerMovement;
        private Rigidbody2D rigidBody;
        private Vector2 lastPosition;
        private uint facing; // 0 down / 1 left / 2 up / 3 right 

        private void Start() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }

            rigidBody = GetComponent<Rigidbody2D>();
            playerMovement = GetComponent<PlayerMovement>();
            anim = GetComponent<Animator>();

            // maxHealth = 6;
            health = maxHealth;
            
            HUDController.instance.UpdateHUD(health);
        }

        private void Damage() {
            hitbox.SetActive(true);
        }
        
        public void DamagePlayer(int _damage) {
            health -= _damage;
            // Player will freeze in attack if they are damaged. Not sure this will fix it
            isAttacking = false;
            HUDController.instance.UpdateHUD(health);
            StartCoroutine(HUDController.instance.Shake(0.4f, 0.15f));

            if (health < 1 && !GameController.instance.playerDead) {
                GameController.instance.playerDead = true;
                GameController.instance.coinsCollected = 0; 
                canMove = false;
                anim.Play("PlayerDeathDown");
                HUDController.instance.FadeIn();
                GameController.instance.ChangeRoom();
            }
        }

        public void OnAttack(InputAction.CallbackContext context) {
            if (context.performed) {
                isAttacking = true;
                canMove = false;
                rigidBody.linearVelocity = Vector2.zero;
                playerMovement.moveInput = Vector2.zero;
                anim.SetBool(IsWalking, false);
                anim.SetTrigger(AttackTrigger);
                Damage();
            }
        }
                
        public void OnInteract(InputAction.CallbackContext context) {
            // context.started gets only when the key is pressed
            if (context.started && canLoot) {
                Objects.Chest.instance.OpenChest();
            }

            if (context.started && canInteract) {
                Utils.Switch.instance.FlipSwitch();
            }

            if (context.started && GameController.instance.masterDoorActive) {
                if (GameController.instance.totalCoins >= GameController.instance.coinsToOpen) {
                    MasterRoomController.instance.OpenDoor();
                }
            }
        }

        public void ResetAttack() {
            canMove = true;
            isAttacking = false;
            anim.SetBool(AttackTrigger, false);
            hitbox.SetActive(false);
        }

        public void FallIntoPit() {
            canMove = false;
            lastPosition = transform.position;
            facing = playerMovement.playerFacing;
            anim.Play("PlayerFall");

            // When facing down push the player further into the pit so the graphics match
            if (facing == 0) {
                transform.position = new Vector2(lastPosition.x, lastPosition.y - 1);
            }
        }

        // Pit respawn
        public void Respawn() {
            // 0 down / 1 left / 2 up / 3 right 
            switch (facing) {
                case 0:
                    transform.position = new Vector2(lastPosition.x, lastPosition.y + 1);
                    break;
                case 1:
                    transform.position = new Vector2(lastPosition.x + 1, lastPosition.y);
                    break;
                case 2:
                    transform.position = new Vector2(lastPosition.x, lastPosition.y - 1);
                    break;
                case 3:
                    transform.position = new Vector2(lastPosition.x - 1, lastPosition.y);
                    break;
            }
            anim.Play("Idle");
            canMove = true;
        }

        public void Reset() {
            health = maxHealth;
            HUDController.instance.UpdateHUD(health);
            HUDController.instance.CalcCoins();
            // Spawn the player facing forward and reset animation back to idle
            anim.Play("Idle");
            anim.SetFloat("LastInputY", -1);
        }
    }
}
