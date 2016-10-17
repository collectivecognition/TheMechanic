using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    private bool dead = false;
    private Vector3 startPosition;
    private float maxDistance = 150f;

    [HideInInspector]
    public Vector3 direction;

    [HideInInspector]
    public float speed;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (!dead) {
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(startPosition, transform.position) >= maxDistance) {
                Die();
            }
        }
	}

    void OnTriggerEnter(Collider collider) {
        if (!dead) {
            if (collider.tag == "Enemy" || collider.tag == "Player") {
                collider.transform.GetComponent<Health>().Hit(Random.Range(1f, 10f));
            }
            Die();
        }
    }

    void Die() {
        GetComponent<Renderer>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        GameObject.Destroy(gameObject, 0.5f);
        dead = true;
    }
}
