using System;
using Controllers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        public bool takeDamage { get; private set; }
        
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

        public void TakeDamage() {
            
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
        }

        public void ResetAttack() {
            canMove = true;
            isAttacking = false;
            anim.SetBool(AttackTrigger, false);
            hitbox.SetActive(false);
        }
    }
}
