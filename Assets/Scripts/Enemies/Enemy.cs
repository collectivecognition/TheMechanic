using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float spawnDelay = 0;
    public float totalHealth;
    public bool initialized = false;

    private Vector3 scale;
    
	public virtual void Start () {
        EnemyManager.Instance.ReactivateAfterDelay(gameObject, spawnDelay);
        scale = gameObject.transform.localScale;
        gameObject.transform.localScale = Vector3.zero;
	}
    
    public void Init() {
        iTween.ScaleTo(gameObject, iTween.Hash("scale", scale, "time", 1f, "oncomplete", "FinishInit"));
    }

    public virtual void FinishInit() {
        initialized = true;
    }

    public virtual void Update() {
        if(!GameManager.Instance.gameActive || !initialized) return;
    }

    protected void OnTriggerStay(Collider collider) {
        if(collider.tag == "Player") {
            collider.transform.GetComponent<HealthBar>().Hit(4f * Time.deltaTime); // FIXME: Make this value configurable
        }
    }
}
