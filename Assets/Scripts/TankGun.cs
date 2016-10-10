using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankGun : MonoBehaviour {
    private float health = 100f;
    private float totalHealth = 100f;

    private Image healthBar;
    private Transform firingPoint;

    void Start () {
        healthBar = transform.Find("Health Canvas/Health").GetComponent<Image>();
        firingPoint = transform.Find("Turret/FiringPoint");
    }

	void Update () {
        
        // FIXME: Move to controls

        if (name == "Player" && Input.GetKeyDown(KeyCode.Space)) {
            Fire();
        }

        healthBar.fillAmount = health / totalHealth;
    }

    public void Fire () {
        Debug.Log("Fire!");
        RaycastHit hit;
        Vector3 pos = firingPoint.position;
        Debug.Log(firingPoint.position);
        Debug.DrawRay(pos, firingPoint.forward * 1000f, Color.red, 101f, true);
        if (Physics.Raycast(pos, firingPoint.forward, out hit, Mathf.Infinity)) {
            if(hit.collider.tag == "Enemy") {
                hit.collider.transform.GetComponent<TankGun>().Hit(15f);
            }
        }

    }

    public void Hit (float damage) {
        health -= damage;
        if(health <= 0f) {
            GameObject.Destroy(gameObject);
        }
    }
}
