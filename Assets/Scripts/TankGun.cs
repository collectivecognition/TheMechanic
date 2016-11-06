using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TankGun : MonoBehaviour {
    private float projectileSpreadAngle = 5f;
    private float lastShotTime = 0;
    private List<GameObject> beamProjectiles = new List<GameObject>();
    private Dictionary<string, GameObject> projectilePrefabs = new Dictionary<string, GameObject>();

    private Transform firingPointTransform;
    private Transform turretTransform;
    private Energy energy;
    private AudioSource audioSource;

    void Awake () {
        firingPointTransform = transform.Find("Turret/FiringPoint");
        turretTransform = transform.Find("Turret");
        energy = PlayerManager.Instance.energy;
        audioSource = GetComponent<AudioSource>();

        projectilePrefabs.Add("Default", Resources.Load<GameObject>("Prefabs/Projectile"));
        projectilePrefabs.Add("Capsule", Resources.Load<GameObject>("Prefabs/CapsuleProjectile"));
        projectilePrefabs.Add("Beam", Resources.Load<GameObject>("Prefabs/Beam"));
    }

	void Update () {
        if (!GameManager.Instance.gameActive) {
            if (beamProjectiles.Count > 0) {
                RemoveBeams();
            }

            return;
        }

        if (BattleManager.Instance.BattleActive && tag == "Player") {

            // Keyboard and analog stick controls

            InventoryItem gun = InventoryManager.Instance.inventory.currentGun;

            switch (gun.type) {
                case InventoryItem.Type.Gun:
                    if (Input.GetMouseButton(0) || Input.GetAxisRaw("TurretVertical") != 0 || Input.GetAxisRaw("TurretHorizontal") != 0) {
                        FireProjectiles();
                    }
                    break;

                case InventoryItem.Type.BeamGun:
                    if (Input.GetMouseButton(0) || Input.GetAxisRaw("TurretVertical") != 0 || Input.GetAxisRaw("TurretHorizontal") != 0) {
                        if (beamProjectiles.Count == 0) {
                            FireBeams();
                        }
                    }else {
                        RemoveBeams();
                    }
                    break;
            }
        }
    }

    public void FireBeams() {
        BeamGunItem gun = (BeamGunItem)InventoryManager.Instance.inventory.currentGun;

        GameObject beam = GameObject.Instantiate(projectilePrefabs["Beam"]);
        beam.GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor", gun.color);

        beamProjectiles.Add(beam);
    }

    public void RemoveBeams() {
        foreach(GameObject beam in beamProjectiles) {
            Destroy(beam);
        }

        beamProjectiles.Clear();
    }

    public void FireProjectiles() {
        GunItem gun =(GunItem)InventoryManager.Instance.inventory.currentGun;

        // Rate limit shots

        if (lastShotTime != 0 && Time.fixedTime - lastShotTime < 1 / gun.fireRate) {
            return;
        }

        lastShotTime = Time.fixedTime;

        // Make sure we have enough energy

        if (energy.current >= gun.energyUsePerShot) {
            for(int ii = 0; ii < gun.projectilesPerShot; ii++) {
                GameObject projectile = GameObject.Instantiate(projectilePrefabs[gun.projectileName]);
                projectile.transform.position = firingPointTransform.position;

                // Spread shots

                if (gun.spread) {
                    float angle = (-projectileSpreadAngle * (gun.projectilesPerShot - 1) / 2 + ii * projectileSpreadAngle); // Space shots out evenly
                    projectile.GetComponent<Projectile>().direction = Quaternion.AngleAxis(angle, Vector3.up) * turretTransform.forward; // Aim based on calculated angle for each shot

                // Non-spread shots

                } else {
                    float projectileSpacing = gun.projectileSpacing; // Space shots out evenly
                    projectile.transform.position += turretTransform.right * (-projectileSpacing * (gun.projectilesPerShot - 1) / 2 + ii * projectileSpacing); // Modify x coord to space out multiple shots
                    projectile.GetComponent<Projectile>().direction = turretTransform.forward; // Aim along turret
                }

                // Set shot attributes

                projectile.GetComponent<Renderer>().material.SetColor("_EmissionColor", gun.color);
                projectile.transform.localScale =  new Vector3(projectile.transform.localScale.x * gun.scale.x, projectile.transform.localScale.y * gun.scale.y, projectile.transform.localScale.z * gun.scale.z);
                projectile.transform.localRotation = Quaternion.Euler(projectile.transform.localRotation.eulerAngles.x, turretTransform.rotation.eulerAngles.y, projectile.transform.localRotation.eulerAngles.z);
                projectile.GetComponent<Projectile>().speed = gun.speed; // FIXME: Only need one reference to <Projectile>
                projectile.GetComponent<Projectile>().minDamage = gun.minDamage;
                projectile.GetComponent<Projectile>().maxDamage = gun.maxDamage;
                projectile.GetComponent<Projectile>().maxDistance = gun.range;

                // Play sound
                AudioClip clip = Resources.Load<AudioClip>("Sounds/" + gun.sound); // FIXME: Caching
                audioSource.PlayOneShot(clip);
            }

            energy.Use(gun.energyUsePerShot); // Consume energy
        }
    }
}
