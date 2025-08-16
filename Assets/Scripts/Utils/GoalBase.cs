using UnityEngine;

namespace Utils {
    public abstract class GoalBase : MonoBehaviour {
        public virtual int numOfEnemies { get; }
        public virtual GameObject switchObj => null;
    }
}