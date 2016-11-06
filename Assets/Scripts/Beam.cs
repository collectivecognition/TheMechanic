using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {
    public LayerMask layerMask;

    private float beamScale = 0f;

    private AudioSource audioSource;
    private AudioSource hitAudioSource;
    private Transform turretTransform;
    private Transform firingPointTransform;
    private Transform sparksTransform;
    private ParticleSystem sparksParticleSystem;

    void Start() {
        audioSource = transform.parent.Find("Audio").GetComponent<AudioSource>();
        hitAudioSource = transform.parent.Find("HitAudio").GetComponent<AudioSource>();

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
            transform.parent.localScale = new Vector3(beamScale, Vector3.Distance(firingPointTransform.position, hit.point) / 2, beamScale);
            sparksTransform.position = hit.point;

            if (!sparksParticleSystem.isPlaying) {
                sparksParticleSystem.Play();
                hitAudioSource.Play();
            }

        } else {
            transform.parent.localScale = new Vector3(beamScale, 1000f, beamScale);
            sparksParticleSystem.Stop(true);
            hitAudioSource.Stop();
        }

        // Scale up beam over time

        beamScale = Mathf.Lerp(beamScale, gun.scale.x, 6f * Time.deltaTime);
    }

    public void OnTriggerStay(Collider collider) {
        BeamGunItem gun = (BeamGunItem)InventoryManager.Instance.inventory.currentGun;

        if (collider.tag == "Enemy" || collider.tag == "Player") {
            collider.GetComponent<HealthBar>().Hit(Random.Range(gun.minDamagePerSecond * Time.deltaTime, gun.maxDamagePerSecond * Time.deltaTime));
        }
    }

    public void OnTriggerLeave(Collider collider) {

    }
}
