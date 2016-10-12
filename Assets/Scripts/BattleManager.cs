using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class BattleManager : Singleton<BattleManager> {
    private Vector3 originalPlayerPosition;
    private Quaternion originalPlayerRotation;
    private List<Participant> participants;
    private Participant player;
    private Participant currentParticipant;
    private float distancePerTurn = 30f;

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

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
    }

    void Update() {

        // FIXME: Test code

        if (Input.GetKeyDown(KeyCode.Z)) { // Start battle
            StartBattle();
        }

        if (Input.GetKeyDown(KeyCode.N)) { // Next turn
            NextTurn();
        }

        if (participants != null) {
            TankDistanceCounter counter = currentParticipant.tank.GetComponent<TankDistanceCounter>();
            float totalDistance = counter.totalDistance;

            if (totalDistance > distancePerTurn) {
                currentParticipant.tank.GetComponent<TankControls>().controllable = false;
            }
        }
    }

    void NextTurn() {

        // participants.ForEach(p => Debug.Log("Participant: " + p.initiative + " " + p.isAlive + " " + p.isPlayer));

        Participant nextParticipant = participants.Find(p => p.isAlive && p.initiative > currentParticipant.initiative);

        if (nextParticipant == null) {
            nextParticipant = participants.First();
        }

        if(currentParticipant == nextParticipant) {

            // Current participant won, as we looped through the entire array
            Debug.Log("Game over man, game over!");
            EndBattle();
        }

        currentParticipant = nextParticipant;
        
        // Handle control state for player

        if(currentParticipant.isPlayer) {
            player.tank.GetComponent<TankDistanceCounter>().Reset();
            player.tank.GetComponent<TankControls>().controllable = true;
            player.tank.GetComponent<TankTurret>().controllable = true;
            player.tank.GetComponent<TankGun>().controllable = true;
        } else {
            player.tank.GetComponent<TankControls>().StopImmediately();
            player.tank.GetComponent<TankControls>().controllable = false;
            player.tank.GetComponent<TankTurret>().controllable = false;
            player.tank.GetComponent<TankGun>().controllable = false;

            // FIXME: Placeholder for enemy AI

            currentParticipant.tank.GetComponent<TankTurret>().AimAt(player.tank.gameObject);
            // currentParticipant.tank.gun.Fire();
            NextTurn();
        }
    }

    public void StartBattle() {

        // Load the battle scene before initializing the battle

        GameManager.Instance.LoadScene("TestBattle", () => {
            participants = new List<Participant>();

            player = new Participant(
                GameObject.Find("Player"),
                0,
                true
            );

            // Save player transform
            // FIXME: Need to preserve which scene the player came from as well

            //originalPlayerPosition = player.tank.transform.position;
            //originalPlayerRotation = player.tank.transform.rotation;

            // Reset player velocity

            player.tank.GetComponent<TankControls>().StopImmediately();

            // Grab a list of spawn points

            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

            // Spawn player at a spawn point

            player.tank.transform.position = spawnPoints[1].transform.position;
            player.tank.GetComponent<TankDistanceCounter>().Reset();

            participants.Add(player);

            // Spawn enemy

            GameObject tankPrefab = Resources.Load<GameObject>("Prefabs/tank");
            GameObject tank = GameObject.Instantiate(tankPrefab);
            tank.transform.position = spawnPoints[0].transform.position;
            tank.transform.rotation = spawnPoints[0].transform.rotation;
            tank.tag = "Enemy";
            Participant participant = new Participant(tank, 1);
            participant.initiative = 1;

            participants.Add(participant);

            tank.GetComponent<TankGun>().OnDie += OnTankDie;

            // FIXME: Better way to spawn multiple enemies

            GameObject tank2 = GameObject.Instantiate(tankPrefab);
            tank2.transform.position = spawnPoints[2].transform.position;
            tank2.transform.rotation = spawnPoints[2].transform.rotation;
            tank2.tag = "Enemy";
            Participant participant2 = new Participant(tank2, 1);
            participant2.initiative = 2;

            participants.Add(participant2);

            tank2.GetComponent<TankGun>().OnDie += OnTankDie;

            // Set initial turn

            participants.OrderBy(p => p.initiative); // Set initial order
            currentParticipant = player;
        });
    }

    private void OnTankDie(GameObject g) {
        Participant participant = participants.Find(p => p.tank == g);
        participant.isAlive = false;
        participant.tank.GetComponent<TankDialog>().Say("I have been destroyed! Woe is me!");
        Debug.Log("Participant died: " + participant.initiative);
    }

    private void EndBattle() {
        participants.ForEach(p => {
            if (!p.isPlayer) {
                GameObject.Destroy(p.tank);
            }
        });

        participants = null;

        player.tank.transform.position = originalPlayerPosition;
        player.tank.transform.rotation = originalPlayerRotation;

        player.tank.GetComponent<TankControls>().controllable = true;
        player.tank.GetComponent<TankTurret>().controllable = true;
        player.tank.GetComponent<TankGun>().controllable = true;
    }
}
