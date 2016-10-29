using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankGun : MonoBehaviour {
    private float projectileSpreadAngle = 5f;
    private float lastShotTime = 0;

    private Transform firingPoint;
    private GameObject projectilePrefab;
    private Transform turretTransform;
    private Energy energy;
    private AudioSource audioSource;

    void Awake () {
        firingPoint = transform.Find("Turret/FiringPoint");
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
        turretTransform = transform.Find("Turret");
        energy = PlayerManager.Instance.energy;
        audioSource = GetComponent<AudioSource>();
    }

	void Update () {
        if (!GameManager.Instance.gameActive) return;

        if (BattleManager.Instance.BattleActive && tag == "Player") {

            // Keyboard and analog stick controls

            if (Input.GetMouseButton(0) || Input.GetAxisRaw("TurretVertical") != 0 || Input.GetAxisRaw("TurretHorizontal") != 0) {
                Fire();
            }
        }
    }

    public void Fire () {
        GunItem gun = InventoryManager.Instance.inventory.currentGun;

        // Rate limit shots

        if (lastShotTime != 0 && Time.fixedTime - lastShotTime < 1 / gun.fireRate) {
            return;
        }

        lastShotTime = Time.fixedTime;

        // Make sure we have enough energy

        if (energy.current >= gun.energyUsePerShot) {
            for(int ii = 0; ii < gun.projectilesPerShot; ii++) {
                GameObject projectile = GameObject.Instantiate(projectilePrefab);
                projectile.transform.position = firingPoint.position;
                //projectile.transform.rotation = turret.rotation;

                // Spread shots

                if (gun.spread) {
                    float angle = (-projectileSpreadAngle * (gun.projectilesPerShot - 1) / 2 + ii * projectileSpreadAngle); // Space shots out evenly
                    projectile.GetComponent<Projectile>().direction = Quaternion.AngleAxis(angle, Vector3.up) * turretTransform.forward; // Aim based on calculated angle for each shot

                // Non-spread shots

                } else {
                    float projectileSpacing = gun.scale.x * 2f; // Space shots out evenly
                    projectile.transform.position += turretTransform.right * (-projectileSpacing * (gun.projectilesPerShot - 1) / 2 + ii * projectileSpacing); // Modify x coord to space out multiple shots
                    projectile.GetComponent<Projectile>().direction = turretTransform.forward; // Aim along turret
                }

                // Set shot attributes

                projectile.GetComponent<Renderer>().material.SetColor("_EmissionColor", gun.color);
                projectile.transform.localScale =  new Vector3(projectile.transform.localScale.x * gun.scale.x, projectile.transform.localScale.y * gun.scale.y, projectile.transform.localScale.z * gun.scale.z);
                projectile.transform.localRotation = turretTransform.rotation;
                projectile.GetComponent<Projectile>().speed = gun.speed; // FIXME: Only need one reference to <Projectile>
                projectile.GetComponent<Projectile>().minDamage = gun.minDamage;
                projectile.GetComponent<Projectile>().maxDamage = gun.maxDamage;

                // Play sound
                AudioClip clip = Resources.Load<AudioClip>("Sounds/" + gun.sound); // FIXME: Caching
                audioSource.PlayOneShot(clip);
            }

            energy.Use(gun.energyUsePerShot); // Consume energy
        }
    }
}
