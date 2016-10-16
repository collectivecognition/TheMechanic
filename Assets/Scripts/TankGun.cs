using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankGun : MonoBehaviour {
    private float energyUsePerShot = 5f;
    private float projectileSpeed = 65f;

    private Transform firingPoint;
    private GameObject projectilePrefab;
    private Transform turret;

    void Start () {
        firingPoint = transform.Find("Turret/FiringPoint");
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
        turret = transform.Find("Turret");
    }

	void Update () {
        
        // FIXME: Move to controls

        if (GameManager.Instance.gameActive && BattleManager.Instance.BattleActive && name == "Player") {
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

        if (energy.Energy >= energyUsePerShot) {
            Vector3 pos = firingPoint.position;

            GameObject projectile = GameObject.Instantiate(projectilePrefab);
            projectile.transform.position = pos;
            projectile.GetComponent<Projectile>().direction = turret.forward;
            projectile.GetComponent<Projectile>().speed = projectileSpeed;

            energy.UseEnergy(energyUsePerShot);
        }
    }
}
