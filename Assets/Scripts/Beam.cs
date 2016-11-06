using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {
    private Transform turretTransform;
    private Transform firingPointTransform;
    private Vector3 collisionPoint = Vector3.zero;

    void Start() {
        firingPointTransform = GameManager.Instance.player.transform.Find("Turret/FiringPoint");
        turretTransform = GameManager.Instance.player.transform.Find("Turret");
    }

	void Update() {
        BeamGunItem gun = (BeamGunItem)InventoryManager.Instance.inventory.currentGun;

        transform.parent.position = firingPointTransform.position;
        transform.parent.localRotation = Quaternion.Euler(transform.parent.localRotation.eulerAngles.x, turretTransform.rotation.eulerAngles.y, transform.parent.localRotation.eulerAngles.z);

        if(collisionPoint == Vector3.zero) {
            transform.parent.localScale = new Vector3(gun.scale.x, 1000f, gun.scale.y);
        }else {
            transform.parent.localScale = new Vector3(gun.scale.x, Vector3.Distance(firingPointTransform.position, collisionPoint), gun.scale.y);
        }
    }

    public void OnTriggerEnter(Collider collider) {
        collisionPoint = collider.transform.position; // FIXME: Calculate actual position
    }

    public void OnTriggerExit(Collider collider) {
        collisionPoint = Vector3.zero;
    }

    public void OnTriggerStay(Collider collider) {
        BeamGunItem gun = (BeamGunItem)InventoryManager.Instance.inventory.currentGun;
        Debug.Log(collider.tag);
        if (collider.tag == "Enemy" || collider.tag == "Player") {
            collider.GetComponent<HealthBar>().Hit(Random.Range(gun.minDamagePerSecond * Time.deltaTime, gun.maxDamagePerSecond * Time.deltaTime));
        }
    }
}
