using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	void Update() {
	    if(Input.GetAxisRaw("Action") != 0) {
            GameManager.Instance.LoadScene("Desert", "HangarDoorSpawnPoint");
        }
	}
}
