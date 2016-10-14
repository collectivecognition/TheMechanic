using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankGun : MonoBehaviour {
    [HideInInspector]
    public bool controllable = false;

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

        if (controllable && name == "Player") {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Fire();
            }

            if (Input.GetMouseButtonDown(0)) {
                Fire();
            }
        }
    }

    public void Fire () {
        //RaycastHit hit;
        Vector3 pos = firingPoint.position;

        // Debug.DrawRay(pos, firingPoint.forward * 1000f, Color.red, 101f, true);

        //if (Physics.Raycast(pos, firingPoint.forward, out hit, Mathf.Infinity)) {
        //    if(hit.collider.tag == "Enemy" || hit.collider.name == "Player") {
        //        hit.collider.transform.GetComponent<TankGun>().Hit(50f);
        //    }
        //}

        GameObject projectile = GameObject.Instantiate(projectilePrefab);
        projectile.transform.position = pos;
        projectile.transform.rotation = turret.rotation;

        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 30f, ForceMode.Impulse);
    }
}
