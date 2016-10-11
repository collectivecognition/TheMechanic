using UnityEngine;
using System.Collections;

public class Tank : ScriptableObject {
    public GameObject gameObject;
    public TankDistanceCounter distanceCounter;
    public TankControls controls;
    public TankGun gun;
    public TankTurret turret;

    public Tank(Transform transform) {
        GameObject tankPrefab = Resources.Load<GameObject>("Prefabs/tank"); // FIXME: Cache?
        gameObject = GameObject.Instantiate(tankPrefab);
        gameObject.transform.position = transform.position;
        gameObject.transform.rotation = transform.rotation;
        gameObject.tag = "Enemy";

        Init(gameObject);
    }

    public Tank(GameObject g) {
        Init(g);
    }

    void Init(GameObject g) {
        gameObject = g;

        distanceCounter = gameObject.GetComponent<TankDistanceCounter>();
        controls = gameObject.GetComponent<TankControls>();
        gun = gameObject.GetComponent<TankGun>();
        turret = gameObject.GetComponent<TankTurret>();
    }

    ~Tank() {
        GameObject.Destroy(gameObject);
    }
}