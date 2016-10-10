using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Car;

public class BattleManager : Singleton<BattleManager> {
    static GameObject player;
    static GameObject battle;
    static GameObject tankPrefab;

    private List<GameObject> tanks;
    private int turn = 0;
    private float distancePerTurn = 30f;

    void Start() {
        player = GameObject.Find("Player");
        battle = GameObject.Find("Battle");
        tankPrefab = Resources.Load<GameObject>("Prefabs/tank");
    }

    void FixedUpdate() {

        // FIXME: Test code

        if (Input.GetKeyDown(KeyCode.Z)) {
            StartBattle();
        }

        if (Input.GetKeyDown(KeyCode.N)) {
            NextTurn();
        }

        if (tanks != null) {
            TankDistanceCounter counter = tanks[turn].GetComponent<TankDistanceCounter>();
            float totalDistance = counter.totalDistance;

            if (totalDistance > distancePerTurn) {
                // tanks[turn].GetComponent<TankControls>().StopImmediately();
                tanks[turn].GetComponent<TankControls>().controlsAreActive = false;
            }
        }
    }

    void NextTurn() {
        turn++;
        if(turn >= tanks.Count) {
            turn = 0;
        }

        Debug.Log("Turn: " + turn);

        if(tanks[turn] == player) {
            tanks[turn].GetComponent<TankDistanceCounter>().Reset();
            player.GetComponent<TankControls>().controlsAreActive = true;
        }else {
            player.GetComponent<TankControls>().StopImmediately();
            player.GetComponent<TankControls>().controlsAreActive = false;
        }
    }

    public void StartBattle() {
        tanks = new List<GameObject>();

        // Reset player velocity

        player.GetComponent<TankControls>().StopImmediately();

        // Grab a list of spawn points

        GameObject[] spawnPoints = battle.FindChildrenByName("SpawnPoint");

        // Spawn player at a spawn point

        player.transform.position = spawnPoints[1].transform.position;
        player.GetComponent<TankDistanceCounter>().Reset();

        tanks.Add(player);

        // Spawn enemy

        GameObject tank = Instantiate(tankPrefab);
        tank.transform.position = spawnPoints[0].transform.position;
        tank.tag = "Enemy";

        tanks.Add(tank);

        turn = 0;
    }
}
