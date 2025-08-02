using UnityEngine;

namespace Controllers {
    public class RoomController : MonoBehaviour {

        public Goal goal;
        [SerializeField] private GameObject chest;
        [SerializeField] private GameObject[] exits;
        [SerializeField] private Vector2[] chestPositions;

        public enum Goal : byte {
            KillAll,
            Key,
            Switch,
            Test
        }

        private uint currentExit;
        private Transform parent;
        private Animator[] anim;
        private SpriteRenderer[] spriteRenderer;

        private void Start() {
            Debug.Log(LevelController.instance.currentRoom);
            anim = new Animator[exits.Length];
            spriteRenderer = new SpriteRenderer[exits.Length];

            for (int i = 0; i < exits.Length; i++) {
                spriteRenderer[i] = exits[i].GetComponent<SpriteRenderer>();
                anim[i] = exits[i].GetComponent<Animator>();
            }

            if (LevelController.instance.currentRoom != 0) {
                ConfigureExit();
                // ConfigureGoal();
                ConfigureChest();
            }
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
                    Debug.Log("Kill all enemies");
                    break;
                case Goal.Key:
                    Debug.Log("Find the key");
                    break;
                case Goal.Switch:
                    Debug.Log("Find the switch");
                    break;
                case Goal.Test:
                    Debug.Log("Just opens the door");
                    OpenDoor();
                    break;
            }
        }

        //Randomly decide and pick a place to spawn a chest in a room
        private void ConfigureChest() {
            int amountToSpawn = Random.Range(0, 2);

            while (amountToSpawn > 0) {
                Vector2 randomPos = chestPositions[Random.Range(0, chestPositions.Length)];
                amountToSpawn--;
                Instantiate(chest, randomPos, Quaternion.identity, transform);
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
    }
}
