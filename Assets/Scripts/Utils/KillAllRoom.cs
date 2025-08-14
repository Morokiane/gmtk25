using UnityEngine;

namespace Utils {
    public class KillAllRoom : GoalBase {
        [SerializeField] private GameObject[] enemies;

        public override int numOfEnemies => enemies.Length;

        // private void Start() {
        //     numOfEnemies = enemies.Length;
        //     Controllers.RoomController.instance.RecalcEnemy();
        // }
    }
}