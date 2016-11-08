using UnityEngine;
using System.Collections;

public class Health {
    private float _current;
    private float max = 100f;

    public float current {
        get {
            return Mathf.Min(_current, max);
        }
        set {
            _current = value;
        }
    }
    public float total;

    public Health(float t) {
        _current = total = t;
    }
}
