using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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
            // Randomize start delay
            float initialDelay = Random.Range(0f, 2f); // You can increase range
            yield return new WaitForSeconds(initialDelay);

            while (true) {
                anim.Play("Spikes");
                yield return new WaitForSeconds(2f);
                anim.Play("SpikesDown");
                yield return new WaitForSeconds(2f);
            }
        }
    }
}