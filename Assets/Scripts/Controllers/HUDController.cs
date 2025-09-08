using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Controllers {
    public class HUDController : MonoBehaviour {
        public static HUDController instance;

        [SerializeField] private TextMeshProUGUI coinNum;
        [SerializeField] private Sprite[] healthHUD;
        [SerializeField] private Image healthImage; // UI Image component in your HUD

        [Header("Cost Window")]
        [SerializeField] private GameObject costWindow;
        [SerializeField] private TextMeshProUGUI coinsCollected;
        [SerializeField] private TextMeshProUGUI costToOpen;
        private bool costWindowActive;

        [Header("Blacksmith Menu")]
        [SerializeField] private GameObject blacksmithMenu;
        [SerializeField] private TextMeshProUGUI itemCost; // Updates when the menu is opened from the GameController
        [SerializeField] private GameObject firstButton; // First button in the blacksmith menu that should be selected

        [Header("Loop Level")]
        [SerializeField] private TextMeshProUGUI loopLevelNum;
        private Animator anim;
        private Camera mainCamera;
        
        public GameObject fade;
        private bool isPaused;

        private void Start() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }

            anim = fade.GetComponent<Animator>();
            mainCamera = Camera.main;

            blacksmithMenu.SetActive(false);
        }
        
        // private void Update() {
        //     if (GameController.instance.MenuOpenCloseInput) {
        //         if (!isPaused) {
        //             Pause();
        //         } else {
        //             Unpause();
        //         }
        //     }
        // }

        public void Test(InputAction.CallbackContext context) {
            if (context.started && MasterRoomController.instance.blacksmithInteract) {
                Debug.Log("Opening blacksmith menu");
            }
            // if (GameController.instance.MenuOpenCloseInput) {
            //     if (!isPaused) {
            //         Pause();
            //     } else {
            //         Unpause();
            //     }
            // }
        }

        public void Pause() {
            isPaused = true;
            Time.timeScale = 0f;

            OpenBlacksmithMenu();
        }

        public void Unpause() {
            isPaused = false;
            Time.timeScale = 1f;

            CloseBlacksmithMenu();
        }

        private void OpenBlacksmithMenu() {
            blacksmithMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstButton);
        }

        private void CloseBlacksmithMenu() {
            blacksmithMenu.SetActive(false);
        }
        
        public void UpdateHUD(int currentHealth) {
            currentHealth = Mathf.Clamp(currentHealth, 0, healthHUD.Length - 1);
            healthImage.sprite = healthHUD[currentHealth];
        }
        
        public IEnumerator Shake(float magnitude, float duration) {
            Vector3 originalPos = mainCamera.transform.position;
            float elapsedTime = 0f;

            while (elapsedTime < duration) {
                float xOffset = Random.Range(-1f, 1f) * magnitude;
                float yOffset = Random.Range(-1f, 1f) * magnitude;

                mainCamera.transform.position = new Vector3(xOffset, yOffset, -1.55f);
                elapsedTime += Time.unscaledDeltaTime;
                yield return 0;
            }
            mainCamera.transform.position = originalPos;
            ResetCamera();
        }
        
        public void ResetCamera() {
            mainCamera.transform.position = new Vector3(0f, 0f, -10f);
        }

        public void FadeOut() {
            anim.Play("FadeOut");
        }

        public void FadeIn() {
            anim.Play("FadeIn");
        }

        public void CalcCoins() {
            coinNum.text = GameController.instance.coinsCollected.ToString();
        }

        public void UpdateLoop() {
            loopLevelNum.text = GameController.instance.loopLevel.ToString();
        }

        public void ShowCostWindow() {
            coinsCollected.text = GameController.instance.totalCoins.ToString();
            costToOpen.text = GameController.instance.coinsToOpen.ToString();
            costWindowActive = !costWindowActive;
            costWindow.SetActive(costWindowActive);
        }

        public void GameOver() {
            anim.Play("FadeIn");
        }
    }
}
