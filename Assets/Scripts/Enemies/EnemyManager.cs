using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager> {
    public List<GameObject> enemies = new List<GameObject>();
    public bool loading = false;
    GameObject teleportEffectPrefab;


    public void Start() {
        teleportEffectPrefab = Resources.Load<GameObject>("Prefabs/TeleportEffect");
    }

    public void ReactivateAfterDelay(GameObject g, float delay) {
        enemies.Add(g);
        g.GetComponent<HealthBar>().OnDie += HandleDeath;

        StartCoroutine(DoReactivate(g, delay));
        loading = false; // Set flag after at least one enemy has loaded
    }

    private IEnumerator DoReactivate(GameObject g, float delay) {
        yield return new WaitForSeconds(delay);
        
        GameObject teleportEffect = (GameObject)Instantiate(teleportEffectPrefab, new Vector3(g.transform.position.x, 0f, g.transform.position.z), teleportEffectPrefab.transform.rotation);

        yield return new WaitForSeconds(0.5f);
        g.SendMessage("Init");

        Destroy(teleportEffect, 2f);
    }

    private void HandleDeath(GameObject g) {
        int index = enemies.FindIndex(otherG => g.GetInstanceID() == otherG.GetInstanceID());
        if(index >= 0) {
            enemies.RemoveAt(index);
        }
    }
}
