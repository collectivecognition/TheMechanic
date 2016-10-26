using UnityEngine;
using System.Collections;

public class BattleShield : MonoBehaviour {
	void Start () {
        GetComponent<Renderer>().material.color = new Color(0f, 3f, 0f, 0f);
        iTween.FadeTo(gameObject, iTween.Hash("time", 0.5f, "alpha", 0.3f));
	}

    public void OnBattleEnd() {
        iTween.FadeTo(gameObject, iTween.Hash("time", 0.5f, "alpha", 0f));
    }
}
