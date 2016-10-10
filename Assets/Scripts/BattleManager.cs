using UnityEngine;
using System.Collections.Generic;

public class BattleManager : Singleton<BattleManager> {
    static GameObject battle;

    private Transform originalPlayerTransform;
    private List<Participant> participants;
    static Participant player;
    private int turn = 0;
    private float distancePerTurn = 30f;

    private struct Participant {
        public Tank tank;
        public bool alive;
        public bool isPlayer;

        public Participant(Tank t, bool i = false) {
            tank = t;
            alive = true;
            isPlayer = i;
        }
    }

    void Start() {
        player = new Participant(
            new Tank(GameObject.Find("Player")),
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
            TankDistanceCounter counter = participants[turn].tank.distanceCounter;
            float totalDistance = counter.totalDistance;

            if (totalDistance > distancePerTurn) {
                participants[turn].tank.controls.controllable = false;
            }
        }
    }

    void NextTurn() {
        turn++;
        if(turn >= participants.Count) {
            turn = 0;
        }

        Debug.Log("Turn: " + turn);

        if(participants[turn].isPlayer) {
            Debug.Log("Player's turn");
            player.tank.distanceCounter.Reset();
            player.tank.controls.controllable = true;
            player.tank.turret.controllable = true;
            player.tank.gun.controllable = true;
        } else {
            Debug.Log("Enemy's turn");
            player.tank.controls.StopImmediately();
            player.tank.controls.controllable = false;
            player.tank.turret.controllable = false;
            player.tank.gun.controllable = false;

            // FIXME: Placeholder for enemy AI

            participants[turn].tank.turret.AimAt(player.tank.gameObject);
            participants[turn].tank.gun.Fire();
            NextTurn();
        }
    }

    public void StartBattle() {
        participants = new List<Participant>(); // FIXME: Memory leak?

        // Save player transform

        originalPlayerTransform = player.tank.gameObject.transform;

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
        Participant participant = new Participant(tank);

        participants.Add(participant);

        turn = 0;
    }

    private void EndBattle() {
        player.tank.transform.position = originalPlayerTransform.position;
        player.tank.transform.rotation = originalPlayerTransform.rotation;
    }
}
