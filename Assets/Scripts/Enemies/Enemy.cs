using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float spawnDelay = 0;
    
	protected void Start () {
        EnemyManager.Instance.ReactivateAfterDelay(gameObject, spawnDelay);
	}
}
