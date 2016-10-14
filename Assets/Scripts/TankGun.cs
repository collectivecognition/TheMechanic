using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankGun : MonoBehaviour {
    [HideInInspector]

    private float energyUsePerShot = 30f;

    private Transform firingPoint;
    private GameObject projectilePrefab;
    private Transform turret;

    public delegate void OnDieEvent(GameObject gameObject);
    public event OnDieEvent OnDie;

    void Start () {
        firingPoint = transform.Find("Turret/FiringPoint");
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
        turret = transform.Find("Turret");
    }

	void Update () {
        
        // FIXME: Move to controls

        if (GameManager.Instance.gameActive && name == "Player") {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Fire();
            }

            if (Input.GetMouseButtonDown(0)) {
                Fire();
            }
        }
    }

    public void Fire () {
        TankEnergy energy = GetComponent<TankEnergy>();

        if (energy.Energy >= 40f) {
            Vector3 pos = firingPoint.position;

            GameObject projectile = GameObject.Instantiate(projectilePrefab);
            projectile.transform.position = pos;
            projectile.transform.rotation = turret.rotation;

            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 30f, ForceMode.Impulse);

            energy.UseEnergy(energyUsePerShot);
        }
    }
}
