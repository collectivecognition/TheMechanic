using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class BattleManager : Singleton<BattleManager> {
    private Vector3 originalPlayerPosition;
    private Quaternion originalPlayerRotation;
    private string originalPlayerScene;
    private List<Participant> participants;
    private Participant player;
    private Participant currentParticipant;
    private float distancePerTurn = 30f;

    public bool BattleActive { get { return battleActive; } }
    private bool battleActive = true;

    private class Participant {
        public GameObject tank;
        public bool isAlive;
        public int initiative;
        public bool isPlayer;

        public Participant(GameObject t, int i, bool p = false) {
            tank = t;
            isAlive = true;
            isPlayer = p;
            initiative = i;
        }

        public static bool operator ==(Participant a, Participant b) {

            // If both are null, or both are same instance, return true.

            if (System.Object.ReferenceEquals(a, b)) {
                return true;
            }

            // If one is null, but not both, return false.

            if (((object)a == null) || ((object)b == null)) {
                return false;
            }

            // Return true if the fields match:

            return a.tank == b.tank;
        }

        public static bool operator !=(Participant a, Participant b) {
            return !(a == b);
        }
    }

    void Start() {
    }

    void Update() {
        if(participants != null) {
            int activeParticipants = participants.FindAll(p => p.isAlive && !p.isPlayer).Count;

            if (activeParticipants == 0) {
                EndBattle();
            }
        }

        // FIXME: Test code

        if (Input.GetKeyDown(KeyCode.Z)) { // Start battle
            StartBattle();
        }
    }

    public void StartBattle() {
        battleActive = true;

        // Save player transform

        Transform t = GameObject.Find("Shared/Player").transform;

        originalPlayerPosition = t.position;
        originalPlayerRotation = t.rotation;
        originalPlayerScene = SceneManager.GetActiveScene().name;

        // Load the battle scene before initializing the battle

        GameManager.Instance.LoadScene("TestBattle", null, () => {
            //participants = new List<Participant>();

            //player = new Participant(
            //    GameObject.Find("Shared/Player"),
            //    0,
            //    true
            //);

            //// Reset player velocity

            //player.tank.GetComponent<TankControls>().StopImmediately();

            //// Grab a list of spawn points

            //GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

            //// Spawn player at a spawn point

            //player.tank.transform.position = spawnPoints[1].transform.position;
            //player.tank.GetComponent<TankDistanceCounter>().Reset();

            //participants.Add(player);

            //// Spawn enemy

            //GameObject tankPrefab = Resources.Load<GameObject>("Prefabs/tank");
            //GameObject tank = GameObject.Instantiate(tankPrefab);
            //tank.transform.position = spawnPoints[0].transform.position;
            //tank.transform.rotation = spawnPoints[0].transform.rotation;
            //tank.tag = "Enemy";
            //tank.name = "TANK1";
            //Participant participant = new Participant(tank, 1);
            //participant.initiative = 1;

            //participants.Add(participant);

            //tank.GetComponent<TankHealth>().OnDie += OnTankDie;

            //// FIXME: Better way to spawn multiple enemies

            //GameObject tank2 = GameObject.Instantiate(tankPrefab);
            //tank2.transform.position = spawnPoints[2].transform.position;
            //tank2.transform.rotation = spawnPoints[2].transform.rotation;
            //tank2.tag = "Enemy";
            //tank2.name = "TANK2";
            //Participant participant2 = new Participant(tank2, 1);
            //participant2.initiative = 2;

            //participants.Add(participant2);

            //tank2.GetComponent<TankHealth>().OnDie += OnTankDie;

            //// Set initial turn

            //participants.OrderBy(p => p.initiative); // Set initial order
            //currentParticipant = player;
        });
    }

    private void OnTankDie(GameObject g) {
        Participant participant = participants.Find(p => p.tank == g);
        participant.isAlive = false;
        // participant.tank.GetComponent<TankDialog>().Say("I have been destroyed! Woe is me!");
        Debug.Log("Participant died: " + participant.initiative);
    }

    private void EndBattle() {
        battleActive = false;

        participants = null;

        GameManager.Instance.LoadScene(originalPlayerScene, null, () => {
            player.tank.transform.position = originalPlayerPosition;
            player.tank.transform.rotation = originalPlayerRotation;
        });

        GameManager.Instance.gameActive = true;
    }
}
