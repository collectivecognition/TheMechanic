using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {
    public LayerMask layerMask;

    private AudioSource audioSource;
    private Transform turretTransform;
    private Transform firingPointTransform;
    private Transform sparksTransform;
    private ParticleSystem sparksParticleSystem;

    void Start() {
        audioSource = transform.parent.Find("Audio").GetComponent<AudioSource>();
        firingPointTransform = GameManager.Instance.player.transform.Find("Turret/FiringPoint");
        turretTransform = GameManager.Instance.player.transform.Find("Turret");
        sparksTransform = transform.parent.Find("Sparks");
        sparksParticleSystem = sparksTransform.GetComponent<ParticleSystem>();
    }

	void Update() {
        BeamGunItem gun = (BeamGunItem)InventoryManager.Instance.inventory.currentGun;

        transform.parent.position = firingPointTransform.position;
        transform.parent.localRotation = Quaternion.Euler(transform.parent.localRotation.eulerAngles.x, turretTransform.rotation.eulerAngles.y, transform.parent.localRotation.eulerAngles.z);

        Ray ray = new Ray(firingPointTransform.position, turretTransform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
            transform.parent.localScale = new Vector3(gun.scale.x, Vector3.Distance(firingPointTransform.position, hit.point) / 2, gun.scale.y);
            sparksTransform.position = hit.point;

            if (!sparksParticleSystem.isPlaying) {
                sparksParticleSystem.Play();
                audioSource.Play();

            }

        } else {
            transform.parent.localScale = new Vector3(gun.scale.x, 1000f, gun.scale.y);
            sparksParticleSystem.Stop(true);
            audioSource.Stop();
        }
    }

    public void OnTriggerStay(Collider collider) {
        BeamGunItem gun = (BeamGunItem)InventoryManager.Instance.inventory.currentGun;

        if (collider.tag == "Enemy" || collider.tag == "Player") {
            audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Hit"));
            collider.GetComponent<HealthBar>().Hit(Random.Range(gun.minDamagePerSecond * Time.deltaTime, gun.maxDamagePerSecond * Time.deltaTime));
        }
    }
}
