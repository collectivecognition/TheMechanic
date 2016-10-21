using UnityEngine;
using System.Collections;

public class Health {
    public float current;
    public float total;

    public Health(float t) {
        current = total = t;
    }
}
