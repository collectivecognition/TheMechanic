using UnityEngine;
using System.Collections;

public class EnemyManager : Singleton<EnemyManager> {
    public void ReactivateAfterDelay(GameObject g, float delay) {
        g.SetActive(false);
        StartCoroutine(DoReactivate(g, delay));
    }

    private IEnumerator DoReactivate(GameObject g, float delay) {
        yield return new WaitForSeconds(delay);
        g.SetActive(true);
    }
}
