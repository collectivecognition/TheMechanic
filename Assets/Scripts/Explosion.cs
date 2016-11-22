using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    public Color color;

    private AudioSource audioSource;

	void Start () {
        audioSource = GetComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("Sounds/Explosion");
        audioSource.PlayOneShot(clip);

        // Spawn shrapnel

        for(int ii = 0; ii < 10; ii++) {
            GameObject gib = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gib.tag = "Gib";
            gib.GetComponent<Renderer>().material.SetFloat("_Mode", 3f); // Use transparent rendering
            gib.transform.localScale = new Vector3(Random.Range(1, 4), Random.Range(1, 4), Random.Range(1, 4));
            gib.transform.position = transform.position + Random.onUnitSphere * 10f;
            Rigidbody rigidbody = gib.AddComponent<Rigidbody>();
            rigidbody.mass = 0.1f;
            rigidbody.AddExplosionForce(100f, transform.position, 50f);
            rigidbody.AddRelativeTorque(Random.insideUnitSphere);
            gib.layer = LayerMask.NameToLayer("Gibs");
            GameObject.Instantiate(gib);

            gib.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
            gib.GetComponent<Renderer>().material.SetColor("_Color", color);
        }

        // Destroy after delay

        Destroy(gameObject, 2f);
	}
}
