using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
	void Start (){
        iTween.RotateAdd(gameObject, iTween.Hash("y", 180f, "time", 1.5f, "loopType", iTween.LoopType.loop, "easeType", iTween.EaseType.linear));
	}

    void OnCollisionEnter(Collision collision) {
        if(collision.collider.name == "Player") {
            GetComponent<ParticleSystem>().Play();
            GetComponent<Renderer>().enabled = false;
            GameObject.Destroy(gameObject, 0.5f);
        }
    }
}
