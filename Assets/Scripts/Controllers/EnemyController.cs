using UnityEngine;
using System.Collections.Generic;
using Utils;

namespace Controllers {
    public class EnemyController : MonoBehaviour {
        public enum StateMachine {
            Patrol,
            Engage,
            Evade
        }

        public Node currentNode;
        public List<Node> path;
        
        public float speed = 3;

        public StateMachine currentState;

        private Player.Player player;
        private AStarController aStarController;

        private void Start() {
            player = Player.Player.instance;
            aStarController = AStarController.instance;
            currentState = StateMachine.Patrol;

            // Find the closest node
            if (currentNode == null) {
                currentNode = aStarController.FindNearestNode(transform.position);
            }

            Debug.Log(player + " " + aStarController + " " + currentState);
        }

        private void Update() {
            switch (currentState) {
                case StateMachine.Patrol:
                    Patrol();
                    break;
                case StateMachine.Engage:
                    Engage();
                    break;
                // case StateMachine.Evade:
                //     Evade();
                //     break;
            }

            bool playerSeen = Vector2.Distance(transform.position, player.transform.position) < 5.0f;
            
            if (!playerSeen && currentState != StateMachine.Patrol/*&& currentHealth > (maxHealth * 20) / 100*/) {
                currentState = StateMachine.Patrol;
                path.Clear();
            } else if (playerSeen && currentState != StateMachine.Engage/*&& currentHealth > (maxHealth * 20) / 100*/) {
                currentState = StateMachine.Engage;
                path.Clear();
            }
            // } else if (currentState != StateMachine.Evade)/*&& currentHealth <= (maxHealth * 20) / 100*/ {
            //     currentState = StateMachine.Evade;
            //     path.Clear();
            // }

            CreatePath();
        }

        public void Patrol() {
            if (path.Count == 0) {
                path = AStarController.instance.GeneratePath(currentNode, AStarController.instance.NodesInScene()[Random.Range(0, AStarController.instance.NodesInScene().Length)]);
                Debug.Log("Generate patrol path " + (path != null ? path.Count : 0));
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

        public void CreatePath() {
            Debug.Log(path.Count);
            if (path.Count > 0) {
                int x = 0;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2), speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f) {
                    currentNode = path[x];
                    path.RemoveAt(x);
                }
            }            
        }
    }
}
