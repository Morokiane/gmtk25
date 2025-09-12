using UnityEngine;
using System.Collections;

namespace Utils {
    public class Bounce : MonoBehaviour {
        [Header("General Settings")]
        [SerializeField] private float upForce = 5f;
        [SerializeField] private float hForce = 3f;
        [Header("Fall Settings")]
        [SerializeField] private float gravityScale = 1f;
        [SerializeField] private float minFallTime = 0.3f;
        [SerializeField] private float maxFallTime = 1.0f;
        [Header("Bounce Settings")]        
        [SerializeField] private float bounceHeight = 0.2f;
        [SerializeField] private float bounceDuration = 0.1f;

        private float stopTime;
        private bool stopped;

        private Rigidbody2D rb;
        private CircleCollider2D circleCollider2D;

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
            circleCollider2D = GetComponent<CircleCollider2D>();
            
            Spawn();
        }

        private void FixedUpdate() {
            if (!stopped && Time.time >= stopTime && rb.linearVelocity.y < 0f) {
                StartCoroutine(LittleBounce());
                stopped = true;
            }
        }

        private void Spawn() {
            stopped = false;
            rb.linearVelocity = Vector2.zero;

            float dir = Random.value < 0.5f ? -1f : 1f;
            upForce = Random.Range(2f, 5f);
            hForce = Random.Range(1f, 2f);

            rb.AddForce(new Vector2(dir * hForce, upForce), ForceMode2D.Impulse);
            rb.gravityScale = gravityScale;

            stopTime = Time.time + Random.Range(minFallTime, maxFallTime);
        }

        private IEnumerator LittleBounce() {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;

            Vector2 start = transform.position;
            Vector2 peak = start + Vector2.up * bounceHeight;

            float t = 0f;
            while (t < 1f) {
                t += Time.deltaTime / bounceDuration;
                float curve = Mathf.Sin(t * Mathf.PI);
                transform.position = Vector2.Lerp(start, peak, curve);
                yield return null;
            }

            transform.position = start;
            circleCollider2D.enabled = true;
        }
    }
}
