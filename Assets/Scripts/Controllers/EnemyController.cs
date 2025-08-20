using UnityEngine;
using System.Collections.Generic;
using Utils;

namespace Controllers {
    public class EnemyController : MonoBehaviour {
        public Node currentNode;
        public List<Node> path = new List<Node>();

        private void Update() {
            CreatePath();
        }

        public void CreatePath() {
            if (path.Count > 0) {
                int x = 0;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2), 3 * Time.deltaTime);

                if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f) {
                    currentNode = path[x];
                    path.RemoveAt(x);
                }
            } else {
                Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.None);
                
                while (path == null || path.Count == 0) {
                    // If this errors its because the AStarController is missing in the level
                    path = AStarController.instance.GeneratePath(currentNode, nodes[Random.Range(0, nodes.Length)]);
                }
            }
        }
    }
}
