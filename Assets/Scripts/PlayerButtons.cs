using UnityEngine;
using System.Collections;

public class PlayerButtons : MonoBehaviour {
    public static bool up;
    public static bool down;
    public static bool left;
    public static bool right;

    void Update() {
        float y = Input.GetAxis("Vertical");

        //Debug.Log("X: " + x + ", Y: " + y);
    }
}
