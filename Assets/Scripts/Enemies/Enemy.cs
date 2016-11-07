using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float spawnDelay = 0;
    public float totalHealth;
    
	protected void Start () {
        EnemyManager.Instance.ReactivateAfterDelay(gameObject, spawnDelay);
	}

    protected void OnTriggerStay(Collider collider) {
        if(collider.tag == "Player") {
            collider.transform.GetComponent<HealthBar>().Hit(40f * Time.deltaTime); // FIXME: Make this value configurable
        }
    }
}
