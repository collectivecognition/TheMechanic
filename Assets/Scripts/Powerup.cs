using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
	void Start (){
        iTween.RotateAdd(gameObject, iTween.Hash("y", 180f, "time", 1.5f, "loopType", iTween.LoopType.loop, "easeType", iTween.EaseType.linear));
	}

    void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == "Player") {
            GetComponent<ParticleSystem>().Play();
            GetComponent<Renderer>().enabled = false;
            GetComponent<Light>().intensity = 10f;
            GetComponent<Light>().range = 10f;
            GameObject.Destroy(gameObject, 0.3f);
        }
    }
}
