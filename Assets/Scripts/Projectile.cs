using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    private bool dead = false;
    private Vector3 startPosition;
    private float maxDistance = 150f;

    public Vector3 direction;
    public float speed;
    public float minDamage;
    public float maxDamage;

    private AudioSource audioSource;
    private AudioClip audioClip;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Sounds/Hit"); // FIXME: Cache this once for all projectiles?
    }
	
	// Update is called once per frame
	public void Update () {
        if (!GameManager.Instance.gameActive) return;

        if (!dead) {
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(startPosition, transform.position) >= maxDistance) {
                Remove();
            }
        }
	}

    public void OnTriggerEnter(Collider collider) {
        if (!dead) {
            if (collider.tag == "Enemy" || collider.tag == "Player") {
                collider.transform.GetComponent<HealthBar>().Hit(Random.Range(minDamage, maxDamage));
                Explode();
            } else if (collider.name == "Shield") {
                Explode();
            }else {
                Remove();
            }
        }
    }

    public void Remove() {
        dead = true;
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0f, "time", 0.15f));
        GameObject.Destroy(gameObject, 0.15f);
    }

    private void Explode() {
        GetComponent<Renderer>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        GameObject.Destroy(gameObject, 0.5f);
        dead = true;
        audioSource.PlayOneShot(audioClip);
    }
}
