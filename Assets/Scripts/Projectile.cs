using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    private bool dead = false;
    private Vector3 startPosition;
    private Vector3 pathPosition;
    public float pathProgress;
    private float pathLength;

    public float maxDistance = 250f; // Provided by the gun
    public Vector3 direction;
    public float speed;
    public float minDamage;
    public float maxDamage;
    public Vector3[] path = null;
    public float waveMagnitude;
    public float waveFrequency;

    private AudioSource audioSource;
    private AudioClip audioClip;

    void Start () {
        startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Sounds/Hit"); // FIXME: Cache this once for all projectiles?
        
        if(path != null) {
            pathLength = iTween.PathLength(path);
            pathPosition = startPosition;
        }
    }
	
	public void Update () {
        if (!GameManager.Instance.gameActive) return;

        if (!dead) {
            // Straight shots

            if(path == null) {
                transform.position += direction * speed * Time.deltaTime;

                if(waveFrequency > 0) {
                    transform.position = transform.position + transform.right * Mathf.Sin(2f * Mathf.PI * Time.time * waveFrequency) * waveMagnitude;
                }
            
            // Pathed shots

            } else {
                pathProgress = Mathf.MoveTowards(pathProgress, pathLength, speed * Time.deltaTime);

                Quaternion directionRotation = Quaternion.FromToRotation(Vector3.forward, direction);
                Vector3 pointOnPath = iTween.PointOnPath(path, pathProgress / pathLength);

                transform.position = pathPosition + directionRotation * pointOnPath;

                if (pathProgress >= pathLength) {
                    pathProgress = 0f;
                    pathPosition = transform.position;
                }
            }

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
            } else {
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
