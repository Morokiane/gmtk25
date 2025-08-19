using UnityEngine;
using System.Collections.Generic;

namespace Utils {
    public class Node : MonoBehaviour {
        public Node cameFrom;
        public List<Node> connections;

        public float gScore;
        public float hScore;

        public float FScore() {
            return gScore + hScore;
        }
    }
}
