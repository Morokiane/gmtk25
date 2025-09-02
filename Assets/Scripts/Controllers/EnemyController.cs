using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

namespace Controllers {
    public class EnemyController : MonoBehaviour {
        public enum StateMachine : byte {
            Patrol,
            Engage,
            Evade,
            Pause
        }

        public Node currentNode;
        public List<Node> path;
        public StateMachine currentState;
        private StateMachine previousState;
        
        [SerializeField] private float speed = 3;
        [SerializeField] private bool startPaused;
        [SerializeField] private float pauseTime;

        private Player.Player player;
        private AStarController aStarController;
        private Enemies.Enemy enemy;
        private bool isStunned;

        private void Start() {
            player = Player.Player.instance;
            enemy = GetComponent<Enemies.Enemy>();
            aStarController = AStarController.instance;

            currentState = startPaused ? StateMachine.Pause : StateMachine.Patrol;

            // Find and assign the closest node
            if (currentNode == null) {
                currentNode = aStarController.FindNearestNode(transform.position);
            }

            if (startPaused) {
                StartCoroutine(PauseForSeconds(pauseTime));
            }
        }

        private void Update() {
            if (isStunned) return;
            
            switch (currentState) {
                case StateMachine.Patrol:
                    Patrol();
                    break;
                case StateMachine.Engage:
                    Engage();
                    break;
                case StateMachine.Evade:
                    Evade();
                    break;
                case StateMachine.Pause:
                    // Do nothing if paused
                    break;
            }

            bool playerSeen = Vector2.Distance(transform.position, player.transform.position) < 10.0f;
            
            if (!playerSeen && currentState != StateMachine.Patrol && enemy.health > 1) {
                currentState = StateMachine.Patrol;
                path.Clear();
            } else if (playerSeen && currentState != StateMachine.Engage && enemy.health > 1) {
                currentState = StateMachine.Engage;
                path.Clear();
            } else if (currentState != StateMachine.Evade && enemy.health <= 1) {
                currentState = StateMachine.Evade;
                path.Clear();
            }

            CreatePath();
        }

        public void Patrol() {
            if (path.Count == 0) {
                path = aStarController.GeneratePath(currentNode, aStarController.NodesInScene()[Random.Range(0, aStarController.NodesInScene().Length)]);
                // Debug.Log("Generate patrol path " + (path != null ? path.Count : 0));
            }
        }

        public void Engage() {
            if (path.Count == 0) {
                path = aStarController.GeneratePath(currentNode, aStarController.FindNearestNode(player.transform.position));
            }
        }

        public void Evade() {
            if (path.Count == 0) {
                path = aStarController.GeneratePath(currentNode, aStarController.FindFurthestNode(player.transform.position));
            }
        }

        public IEnumerator Pause() {
            if (path.Count == 0) {
                yield return new WaitForSeconds(pauseTime);
                currentState = StateMachine.Patrol;
                startPaused = false;
            }
        }

        public void CreatePath() {
            if (path.Count > 0) {
                int x = 0;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2), speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f) {
                    currentNode = path[x];
                    path.RemoveAt(x);
                }
            }            
        }

        public void Stun(float duration) {
            if (!isStunned) {
                StartCoroutine(StunRoutine(duration));
            }
        }

        private IEnumerator StunRoutine(float duration) {
            isStunned = true;
            previousState = currentState;
            currentState = StateMachine.Pause;
            path.Clear();

            yield return new WaitForSeconds(duration);

            currentState = previousState;
            isStunned = false;
        }

        private IEnumerator PauseForSeconds(float duration) {
            isStunned = true;
            currentState = StateMachine.Pause;

            yield return new WaitForSeconds(duration);

            currentState = StateMachine.Patrol;
            isStunned = false;
            startPaused = false;
        }
    }
}
