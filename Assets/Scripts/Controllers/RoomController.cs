using UnityEngine;
using Utils;
using System.Collections;

namespace Controllers {
    public class RoomController : MonoBehaviour {
        public static RoomController instance;

        public Goal goal;
        [SerializeField] private GameObject[] exits;
        [Header("Chest Settings")]
        [SerializeField] private GameObject chest;
        [SerializeField] private int chestSpawnChance = 10;
        [SerializeField] private GameObject[] chestPositions;
        [Tooltip("Drag the script that defines the goal")]
        [SerializeField] private GoalBase goalScript;
        [Header("Enemy Settings")]
        [SerializeField] private int numOfSpawns;
        [SerializeField] private GameObject spawnsplosion;
        [SerializeField] private GameObject[] availableEnemies;
        [SerializeField] private GameObject[] enemySpawnLocations;
        [SerializeField] private float minDelay = 0.1f;
        [SerializeField] private float maxDelay = 1.0f;
        [SerializeField] private float spawnEffectDuration = 0.5f;
        
        public enum Goal : byte {
            KillAll,
            Key,
            Switch,
            Test,
            Dead
        }

        private int numOfEnemies;
        private Vector2 spawnLocation;
        private Animator[] anim;
        private SpriteRenderer[] spriteRenderer;

        private void Start() {
            instance = this;
            // Debug.Log(LevelController.instance.currentRoom);
            anim = new Animator[exits.Length];
            spriteRenderer = new SpriteRenderer[exits.Length];

            for (int i = 0; i < exits.Length; i++) {
                spriteRenderer[i] = exits[i].GetComponent<SpriteRenderer>();
                anim[i] = exits[i].GetComponent<Animator>();
            }

            if (LevelController.instance.currentRoom != 0) {
                ConfigureExit();
                ConfigureGoal();
                if (chest != null) {
                    ConfigureChest();
                }
                StartCoroutine(ConfigureEnemies());
            }

            // Turn off the placeholder graphic for chests
            foreach (GameObject chest in chestPositions) {
                SpriteRenderer spriteRenderer = chest.GetComponent<SpriteRenderer>();
                if (spriteRenderer) {
                    spriteRenderer.enabled = false;
                }
            }
            
            Debug.Log(LevelController.instance.currentRoom);
        }

        private void ConfigureExit() {
            // Exits are 0 N, 1 E, 2 S, 3 W
            switch (LevelController.instance.currentRoom) {
                case 1:
                    spriteRenderer[0].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(-5.46f, 0f);
                    break;
                case 2:
                    spriteRenderer[0].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(0f, -2.79f);
                    break;
                case 3:
                    spriteRenderer[3].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(0f, -2.79f);
                    break;
                case 4:
                    spriteRenderer[3].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(5.45f, 0f);
                    break;
                case 5:
                    spriteRenderer[2].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(5.45f, 0f);
                    break;
                case 6:
                    spriteRenderer[2].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(0f, 2.59f);
                    break;
                case 7:
                    spriteRenderer[1].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(0f, 2.59f);
                    break;
                case 8: // This reloads the master room
                    Player.Player.instance.transform.position = new Vector2(0f, 0f);
                    Destroy(LevelController.instance.currentRoomInstance);
                    LevelController.instance.currentRoomInstance = Instantiate(LevelController.instance.rooms[0], transform.position, Quaternion.identity);
                    LevelController.instance.currentRoom = 0;
                    break;
            }
        }

        private void ConfigureGoal() {
            // goal = (Goal)Random.Range(0, System.Enum.GetValues(typeof(Goal)).Length);
            switch (goal) {
                case Goal.KillAll:
                    numOfEnemies = numOfSpawns;
                    // Debug.Log("Kill all enemies");
                    break;
                case Goal.Key:
                    // Debug.Log("Find the key");
                    break;
                case Goal.Switch:
                    // Debug.Log("Find the switch");
                    break;
                case Goal.Test:
                    // Debug.Log("Just opens the door");
                    OpenDoor();
                    break;
            }
        }

        // Randomly decide and pick a place to spawn a chest in a room
        private void ConfigureChest() {
            // roll a number between 0 and 100
            int roll = Random.Range(0, 100);

            if (roll < chestSpawnChance) {
                // pick random spawn point
                int index = Random.Range(0, chestPositions.Length);
                GameObject spawnPoint = chestPositions[index];

                if (spawnPoint != null) {
                    Instantiate(chest, spawnPoint.transform.position, Quaternion.identity, transform);
                }
            }
        }

        private IEnumerator ConfigureEnemies() {
            // Wait for the room to load and the fade to end
            yield return new WaitForSeconds(1.5f);

            int spawnsRemaining = numOfSpawns;

            while (spawnsRemaining > 0) {
                // pick random spawn point
                GameObject spawnPoint = enemySpawnLocations[Random.Range(0, enemySpawnLocations.Length)];

                if (spawnPoint != null) {
                    // pick random enemy prefab
                    GameObject enemyPrefab = availableEnemies[Random.Range(0, availableEnemies.Length)];
                    GameObject effect = Instantiate(spawnsplosion, spawnPoint.transform.position, Quaternion.identity, transform);

                    yield return new WaitForSeconds(spawnEffectDuration);

                    Destroy(effect);
                    Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity, transform);

                    float delay = Random.Range(minDelay, maxDelay);
                    yield return new WaitForSeconds(delay);

                    spawnsRemaining--;
                }
            }
        }
        
        private void OpenDoor() {
            switch (LevelController.instance.currentRoom) {
                case 1:
                    anim[0].Play("DungeonDoorTop");
                    break;
                case 2:
                    anim[0].Play("DungeonDoorTop");
                    break;
                case 3:
                    anim[3].Play("DungeonDoorWest");
                    break;
                case 4:
                    anim[3].Play("DungeonDoorWest");
                    break;
                case 5:
                    anim[2].Play("DungeonDoorSouth");
                    break;
                case 6:
                    anim[2].Play("DungeonDoorSouth");
                    break;
                case 7:
                    anim[1].Play("DungeonDoorEast");
                    break;
            }
        }
        
        // This is for the room goal of killing all enemies
        public void RecalcEnemy() {
            numOfEnemies--;
            if (numOfEnemies <= 0) {
                OpenDoor();
            //     Debug.Log("Door should open");
            }
        }
        
        public void SwitchUsed() {
            OpenDoor();
        }
    }
}
