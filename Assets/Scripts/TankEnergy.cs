using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankEnergy : MonoBehaviour {
    public float Energy { get { return energy;}  }

    private float energy = 100f;
    private float totalEnergy = 100f;
    private float rechargeRate = 10f;
    private Image energyBar;

    void Start() {
        energyBar = transform.Find("EnergyCanvas/Energy").GetComponent<Image>();
    }

    void Update() {
        energyBar.fillAmount = energy / totalEnergy;

        energy += rechargeRate * Time.deltaTime;
        energy = Mathf.Clamp(energy, 0, 100);
    }

    public void UseEnergy(float amount) {
        energy -= amount;
    }
}
