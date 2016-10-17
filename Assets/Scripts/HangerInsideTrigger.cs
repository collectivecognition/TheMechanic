using UnityEngine;
using System.Collections;

public class HangerInsideTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider collider) {
        GameManager.Instance.dialogue.Say("Game saved successfuly...\n\nTODO: Actually save game...");
    }
}
