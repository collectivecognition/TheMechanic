using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float spawnDelay = 0;

    protected bool spawned = false;

	protected void Start () {
        StartCoroutine(Spawn());
        // gameObject.SetActive(false);
	}

    IEnumerator Spawn() {
        yield return new WaitForSeconds(spawnDelay);
        gameObject.SetActive(true);
        // spawned = true;
    }
}
