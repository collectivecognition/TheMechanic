using UnityEngine;
using System.Collections;

public class HangarTank : MonoBehaviour {
    public void OnInteraction() {
        GameManager.Instance.LoadScene("Desert");
    }
}
