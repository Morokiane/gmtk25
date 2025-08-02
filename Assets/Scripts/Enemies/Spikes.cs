using System;
using System.Collections;
using UnityEngine;

namespace Enemies {
    public class Spikes : MonoBehaviour {

        [SerializeField] private int damage;
        private BoxCollider2D boxCollider;
        private Animator anim;

        private void Start() {
            boxCollider = GetComponent<BoxCollider2D>();
            anim = GetComponent<Animator>();
            StartCoroutine(ActivateSpikes());
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
               Player.Player.instance.DamagePlayer(damage);
            }
        }

        public void EnableSpikes() {
            boxCollider.enabled = true;
        }

        private IEnumerator ActivateSpikes() {
            yield return new WaitForSeconds(2);
            anim.Play("Spikes");
            // Controllers.HUDController.instance.ResetCamera();
            yield return new WaitForSeconds(2);
            anim.Play("SpikesDown");
            // Controllers.HUDController.instance.ResetCamera();
            StartCoroutine(ActivateSpikes());
        }
    }
}