using UnityEngine;

namespace Controllers {
    public class RoomController : MonoBehaviour {

        public Goal goal;
        [SerializeField] private GameObject[] exits;

        public enum Goal : byte {
            KillAll,
            Key,
            Switch,
            Test
        }

        private uint currentExit;
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
                ConfigureGoal();
            }
        }

        private void ConfigureExit() {
            // Debug.Log("Current room number: " + LevelController.instance.currentRoom);

            // Exits are 0 N, 1 E, 2 S, 3 W
            switch (LevelController.instance.currentRoom) {
                case 1:
                    spriteRenderer[0].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(-5.46f, 0f);
                    // Debug.Log("Current room is 1");
                    break;
                case 2:
                    spriteRenderer[0].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(0f, -2.79f);
                    // Debug.Log("Current room is 2");
                    break;
                case 3:
                    spriteRenderer[3].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(0f, -2.79f);
                    // Debug.Log("Current room is 3");
                    break;
                case 4:
                    spriteRenderer[3].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(5.45f, 0f);
                    // Debug.Log("Current room is 4");
                    break;
                case 5:
                    spriteRenderer[2].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(5.45f, 0f);
                    // Debug.Log("Current room is 5");
                    break;
                case 6:
                    spriteRenderer[2].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(0f, 2.59f);
                    // Debug.Log("Current room is 6");
                    break;
                case 7:
                    spriteRenderer[1].enabled = true;
                    Player.Player.instance.transform.position = new Vector2(0f, 2.59f);
                    LevelController.instance.currentRoom = -1;
                    // Debug.Log("Current room is 7");
                    break;
                case 8:
                    Player.Player.instance.transform.position = new Vector2(0f, 0f);
                    // LevelController.instance.currentRoom = 0;
                    Debug.Log("jdfkdla;j");
                    break;
            }
        }

        private void ConfigureGoal() {
            switch (goal) {
                case Goal.KillAll:
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
                    Test();
                    break;
            }
        }

        private void Test() {
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
