using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleManager : Singleton<BattleManager> {
    static GameObject battle;

    private Vector3 originalPlayerPosition;
    private Quaternion originalPlayerRotation;
    private List<Participant> participants;
    private Participant player;
    private Participant currentParticipant;
    private float distancePerTurn = 30f;

    private class Participant {
        public Tank tank;
        public bool isAlive;
        public int initiative;
        public bool isPlayer;

        public Participant(Tank t, int i, bool p = false) {
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

            return a.tank.gameObject == b.tank.gameObject;
        }

        public static bool operator !=(Participant a, Participant b) {
            return !(a == b);
        }
    }

    void Start() {
        player = new Participant(
            new Tank(GameObject.Find("Player")),
            0,
            true
        );
        battle = GameObject.Find("Battle");
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
            TankDistanceCounter counter = currentParticipant.tank.distanceCounter;
            float totalDistance = counter.totalDistance;

            if (totalDistance > distancePerTurn) {
                currentParticipant.tank.controls.controllable = false;
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
            player.tank.distanceCounter.Reset();
            player.tank.controls.controllable = true;
            player.tank.turret.controllable = true;
            player.tank.gun.controllable = true;
        } else {
            player.tank.controls.StopImmediately();
            player.tank.controls.controllable = false;
            player.tank.turret.controllable = false;
            player.tank.gun.controllable = false;

            // FIXME: Placeholder for enemy AI

            currentParticipant.tank.turret.AimAt(player.tank.gameObject);
            // currentParticipant.tank.gun.Fire();
            NextTurn();
        }
    }

    public void StartBattle() {
        participants = new List<Participant>();

        // Save player transform

        originalPlayerPosition = player.tank.gameObject.transform.position;
        originalPlayerRotation = player.tank.gameObject.transform.rotation;

        // Reset player velocity

        player.tank.controls.StopImmediately();

        // Grab a list of spawn points

        GameObject[] spawnPoints = battle.FindChildrenByName("SpawnPoint");

        // Spawn player at a spawn point

        player.tank.gameObject.transform.position = spawnPoints[1].transform.position;
        player.tank.distanceCounter.Reset();

        participants.Add(player);

        // Spawn enemy

        Tank tank = new Tank(spawnPoints[0].transform);
        Participant participant = new Participant(tank, 1);

        participants.Add(participant);

        tank.gun.OnDie += OnTankDie;

        // FIXME: Better way to spawn multiple enemies

        tank = new Tank(spawnPoints[2].transform);
        participant = new Participant(tank, 2);

        participants.Add(participant);

        tank.gun.OnDie += OnTankDie;

        // Set initial turn

        participants.OrderBy(p => p.initiative); // Set initial order
        currentParticipant = player;
    }

    private void OnTankDie(GameObject g) {
        Participant participant = participants.Find(p => p.tank.gameObject == g);
        participant.isAlive = false;
        Debug.Log("Participant died: " + participant.initiative);
    }

    private void EndBattle() {
        player.tank.gameObject.transform.position = originalPlayerPosition;
        player.tank.gameObject.transform.rotation = originalPlayerRotation;

        player.tank.controls.controllable = true;
        player.tank.turret.controllable = true;
        player.tank.gun.controllable = true;
    }
}
