using UnityEngine;

namespace Utils {
    public class KillAllRoom : GoalBase {
        [SerializeField] private GameObject[] enemies;

        public override int numOfEnemies => enemies.Length;

        private void Start() {
            foreach (GameObject enemy in enemies) {
                if (enemy != null) {
                    // Replace EnemyScript with the actual name of your enemy's script
                    Enemies.Enemy enemyScript = enemy.GetComponent <Enemies.Enemy>();

                    if (enemyScript != null) {
                        enemyScript.needToCount = true;
                    }
                }
            }
        }
    }
}