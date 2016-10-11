using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankGun : MonoBehaviour {
    [HideInInspector]
    public bool controllable = false;

    private float health = 100f;
    private float totalHealth = 100f;

    private Image healthBar;
    private Transform firingPoint;

    public delegate void OnDieEvent(GameObject gameObject);
    public event OnDieEvent OnDie;

    void Start () {
        healthBar = transform.Find("Health Canvas/Health").GetComponent<Image>();
        firingPoint = transform.Find("Turret/FiringPoint");
    }

	void Update () {
        
        // FIXME: Move to controls

        if (controllable && name == "Player" && Input.GetKeyDown(KeyCode.Space)) {
            Fire();
        }

        healthBar.fillAmount = health / totalHealth; // FIXME: Separate healthBar class
    }

    public void Fire () {
        RaycastHit hit;
        Vector3 pos = firingPoint.position;
        Debug.DrawRay(pos, firingPoint.forward * 1000f, Color.red, 101f, true);
        if (Physics.Raycast(pos, firingPoint.forward, out hit, Mathf.Infinity)) {
            if(hit.collider.tag == "Enemy" || hit.collider.name == "Player") {
                hit.collider.transform.GetComponent<TankGun>().Hit(50f);
            }
        }

        controllable = false; // FIXME: Should be controlled elsewhere, gun should not know how many times it's allowed to shoot
    }

    // FIXME: Should be handled in it's own class, maybe TankHealth?

    public void Hit (float damage) {
        health -= damage;
        if(health <= 0f) {
            // GameObject.Destroy(gameObject);
            OnDie(gameObject);
        }
    }
}
