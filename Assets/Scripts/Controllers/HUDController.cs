using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers {
    public class HUDController : MonoBehaviour {
        public static HUDController instance;

        [SerializeField] private TextMeshProUGUI coinNum;
        [SerializeField] private Sprite[] healthHUD;
        [SerializeField] private Image healthImage; // UI Image component in your HUD
        
        private Animator anim;
        
        public GameObject fade;
        
        private void Start() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
            
            anim = fade.GetComponent<Animator>();
        }
        
        public void UpdateHUD(int currentHealth) {
            currentHealth = Mathf.Clamp(currentHealth, 0, healthHUD.Length - 1);
            healthImage.sprite = healthHUD[currentHealth];
        }
        
        private void UpdateHUD() {
            int health = Player.Player.instance.health;

            // Clamp to avoid out-of-range errors
            health = Mathf.Clamp(health, 0, healthHUD.Length - 1);

            healthImage.sprite = healthHUD[health];
        }


        public void FadeOut() {
            anim.Play("FadeOut");
        }
        
        public void FadeIn() {
            anim.Play("FadeIn");
        }

        public void CalcCoins() {
            coinNum.text = LevelController.instance.coins.ToString();
        }
    }
}
