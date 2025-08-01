using System;
using UnityEngine;

namespace Controllers {
    public class HUDController : MonoBehaviour {
        public static HUDController instance;

        private Animator anim;
        
        public GameObject fade;
        
        private void Start() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
            
            anim =  fade.GetComponent<Animator>();
        }

        public void FadeOut() {
            anim.Play("FadeOut");
        }
        
        public void FadeIn() {
            anim.Play("FadeIn");
        }
    }
}
