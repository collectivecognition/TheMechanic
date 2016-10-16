using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankGun : MonoBehaviour {
    private enum ProjectileType {
        Shot,
        Beam,
        Missile
    };

    private float energyUsePerShot = 5f;
    private float projectileSpeed = 50f;
    private float projectileSize = 1f;
    private Color projectileColor = new Color(1f, 0.5f, 0f, 1f);
    private int projectilesPerShot = 14;
    private bool projectileSpread = true;
    private float projectileSpreadAngle = 5f;
    private float fireRate = 0.25f;

    private float lastShotTime = 0;

    private ProjectileType projectileType = ProjectileType.Shot;

    private Transform firingPoint;
    private GameObject projectilePrefab;
    private Transform turret;
    private TankEnergy energy;

    void Awake () {
        firingPoint = transform.Find("Turret/FiringPoint");
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
        turret = transform.Find("Turret");
        energy = GetComponent<TankEnergy>();
    }

	void Update () {
        if (GameManager.Instance.gameActive && BattleManager.Instance.BattleActive && name == "Player") {
            if (Input.GetAxis("Fire1") != 0) {
                Fire();
            }

            if (Input.GetKeyDown(KeyCode.R)) {
                Randomize();
            }
        }
    }

    public void Randomize() {
        energyUsePerShot = Random.Range(1f, 10f);
        projectileSpeed = Random.Range(10f, 80f);
        projectileSize = Random.Range(0.75f, 1.5f);
        projectileColor = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
        projectilesPerShot = Random.Range(1, 5);
        projectileSpread = Random.Range(0, 1) == 0 ? true : false;

        // FIXME: Adjusting this seems to introduce some weird behavior
        //fireRate = Random.Range(0.25f, 5f);
    }

public void Fire () {

        // Rate limit shots

        if (lastShotTime != 0 && Time.fixedTime - lastShotTime < fireRate) {
            return;
        }

        lastShotTime = Time.fixedTime;

        // Make sure we have enough energy

        if (energy.Energy >= energyUsePerShot) {
            for(int ii = 0; ii < projectilesPerShot; ii++) {
                GameObject projectile = GameObject.Instantiate(projectilePrefab);
                projectile.transform.position = firingPoint.position;

                // Spread shots

                if (projectileSpread) {
                    float angle = (-projectileSpreadAngle * (projectilesPerShot - 1) / 2 + ii * projectileSpreadAngle); // Space shots out evenly
                    projectile.GetComponent<Projectile>().direction = Quaternion.AngleAxis(angle, Vector3.up) * turret.forward; // Aim based on calculated angle for each shot

                // Non-spread shots

                } else {
                    float projectileSpacing = projectileSize + 1f; // Space shots out evenly
                    projectile.transform.position += turret.right * (-projectileSpacing * (projectilesPerShot - 1) / 2 + ii * projectileSpacing); // Modify x coord to space out multiple shots
                    projectile.GetComponent<Projectile>().direction = turret.forward; // Aim along turret
                }

                // Set shot attributes

                projectile.GetComponent<Renderer>().material.SetColor("_TintColor", projectileColor);
                projectile.GetComponent<Light>().color = projectileColor;
                projectile.GetComponent<Projectile>().speed = projectileSpeed;
                projectile.transform.localScale *= projectileSize;
            }

            energy.UseEnergy(energyUsePerShot); // Consume energy
        }
    }
}
