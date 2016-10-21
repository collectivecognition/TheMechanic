using UnityEngine;
using System.Collections;

public class Energy {
    public float current;
    public float total;
    public float chargeRate = 15f;

    public Energy(float t) {
        current = total = t;
    }

    public void Charge() {
        current += chargeRate * Time.deltaTime;
        current = Mathf.Clamp(current, 0, total);
    }

    public void Use(float amount) {
        if (BattleManager.Instance.BattleActive) {
            current -= amount;
        }
    }
}
