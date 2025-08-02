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
        private Camera mainCamera;
        
        public GameObject fade;
        
        private void Start() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
            
            anim = fade.GetComponent<Animator>();
            mainCamera = Camera.main;
        }
        
        public void UpdateHUD(int currentHealth) {
            currentHealth = Mathf.Clamp(currentHealth, 0, healthHUD.Length - 1);
            healthImage.sprite = healthHUD[currentHealth];
        }
        
        // private void UpdateHUD() {
        //     int health = Player.Player.instance.health;
        //
        //     // Clamp to avoid out-of-range errors
        //     health = Mathf.Clamp(health, 0, healthHUD.Length - 1);
        //
        //     healthImage.sprite = healthHUD[health];
        // }

        public IEnumerator Shake(float _magnitude, float _duration) {
            Vector3 originalPos = mainCamera.transform.position;
            float elapsedTime = 0f;

            while (elapsedTime < _duration) {
                float xOffset = UnityEngine.Random.Range(-1f, 1f) * _magnitude;
                float yOffset = UnityEngine.Random.Range(-1f, 1f) * _magnitude;

                mainCamera.transform.position = new Vector3(xOffset, yOffset, -1.55f);
                elapsedTime += Time.unscaledDeltaTime;
                yield return 0;
            }
            mainCamera.transform.position = originalPos;
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
