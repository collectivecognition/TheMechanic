using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    private bool dead = false;

	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().Stop();
        GetComponent<ParticleSystem>().loop = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision) {
        if (!dead) { 
            GetComponent<Renderer>().enabled = false;
            GetComponent<ParticleSystem>().Play();
            GetComponent<Rigidbody>().isKinematic = true;
            GameObject.Destroy(gameObject, 0.5f);
            dead = true;

            if (collision.collider.tag == "Enemy" || collision.collider.name == "Player") {
                collision.collider.transform.GetComponent<TankHealth>().Hit(50f);
            }
        }
    }
}
