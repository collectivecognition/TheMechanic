using UnityEngine;
using System.Collections;

public class Is : MonoBehaviour {

	public static bool APlayer(GameObject g) {
        return g.tag == "Player";
	}

    public static bool APlayerTank(GameObject g) {
        return Is.APlayer(g) && g.name.Contains("PlayerTank");
    }
}
