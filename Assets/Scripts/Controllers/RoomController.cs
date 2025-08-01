using System;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
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
        private Animator anim;
        private SpriteRenderer spriteRenderer;

        private void Start() {
            // Debug.Log(LevelController.instance.currentRoom);
            anim = exits[0].GetComponent<Animator>();
            spriteRenderer = exits[0].GetComponent<SpriteRenderer>();

            if (LevelController.instance.currentRoom != 0) {
                ConfigureExit();
                ConfigureGoal();
            }
        }

        private void ConfigureExit() {
            Debug.Log("Current room number: " + LevelController.instance.currentRoom);

            // Exits are 0 N, 1 E, 2 S, 3 W
            switch (LevelController.instance.currentRoom) {
                case 1:
                    // exits[0].SetActive(true);
                    spriteRenderer.enabled = true;
                    currentExit = 0;
                    Player.Player.instance.transform.position = new Vector2(-5.46f, 0f);
                    break;
            }
        }

        private void ConfigureGoal() {
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
                    Test();
                    break;
            }
        }

        private void Test() {
            if (currentExit == 0) {
                anim.Play("DungeonDoorTop");
            }
        }
    }
}
