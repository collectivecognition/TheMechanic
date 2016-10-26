using UnityEngine;
using System;
using System.Collections;

public class Wait : MonoBehaviour {
    public static IEnumerator ForSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
    }
}
