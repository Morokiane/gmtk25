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
        public bool canMove = true;
        public bool isAttacking;
        public bool canLoot;
        public bool takeDamage;
        public bool canInteract;
        
        private Animator anim;
        private PlayerMovement playerMovement;
        private Rigidbody2D rb;

        private void Start() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }

            rb = GetComponent<Rigidbody2D>();
            playerMovement = GetComponent<PlayerMovement>();
            anim = GetComponent<Animator>();

            maxHealth = 6;
            health = maxHealth;
            
            HUDController.instance.UpdateHUD(health);
        }

        private void Damage() {
            hitbox.SetActive(true);
        }
        
        public void DamagePlayer(int _damage) {
            health -= _damage;
            HUDController.instance.UpdateHUD(health);
            StartCoroutine(HUDController.instance.Shake(0.4f, 0.15f));
            
            if (health <= 0) {
                canMove = false;
                anim.Play("PlayerDeathDown");
                HUDController.instance.FadeIn();
            }
        }

        public void OnAttack(InputAction.CallbackContext context) {
            if (context.performed) {
                isAttacking = true;
                canMove = false;
                rb.linearVelocity = Vector2.zero;
                playerMovement.moveInput = Vector2.zero;
                anim.SetBool(IsWalking, false);
                anim.SetTrigger(AttackTrigger);
                Damage();
            }
        }
                
        public void OnInteract(InputAction.CallbackContext context) {
            // context.started gets only when the key is pressed
             if (context.started && canLoot) {
                Utils.Chest.instance.OpenChest();
             }

             if (context.started && canInteract) {
                 Utils.Switch.instance.FlipSwitch();
             }
        }

        public void ResetAttack() {
            canMove = true;
            isAttacking = false;
            anim.SetBool(AttackTrigger, false);
            hitbox.SetActive(false);
        }
    }
}
