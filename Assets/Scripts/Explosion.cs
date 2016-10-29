using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    
	void Start () {
	    
        // Spawn shrapnel

        for(int ii = 0; ii < 20; ii++) {
            GameObject gib = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gib.tag = "Gib";
            gib.transform.localScale = new Vector3(Random.Range(1, 4), Random.Range(1, 4), Random.Range(1, 4));
            gib.transform.position = transform.position + Random.onUnitSphere * 10f;
            Rigidbody rigidbody = gib.AddComponent<Rigidbody>();
            rigidbody.mass = 0.1f;
            rigidbody.AddExplosionForce(100f, transform.position, 50f);
            rigidbody.AddRelativeTorque(Random.insideUnitSphere);
            gib.layer = LayerMask.NameToLayer("Gibs");
            GameObject.Instantiate(gib);
        }

        // Destroy after delay

        Destroy(gameObject, 2f);
	}
}
