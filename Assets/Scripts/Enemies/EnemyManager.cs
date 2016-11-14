using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager> {
    public List<GameObject> enemies = new List<GameObject>();
    public bool loading = false;

    public void ReactivateAfterDelay(GameObject g, float delay) {
        enemies.Add(g);
        g.GetComponent<HealthBar>().OnDie += HandleDeath;

        g.SetActive(false);
        StartCoroutine(DoReactivate(g, delay));

        loading = false; // Set flag after at least one enemy has loaded
    }

    private IEnumerator DoReactivate(GameObject g, float delay) {
        yield return new WaitForSeconds(delay);
        g.SetActive(true);
    }

    private void HandleDeath(GameObject g) {
        int index = enemies.FindIndex(otherG => g.GetInstanceID() == otherG.GetInstanceID());
        if(index >= 0) {
            enemies.RemoveAt(index);
        }
    }
}
