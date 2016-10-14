using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankEnergy : MonoBehaviour {
    private float energy = 100f;
    private float totalEnergy = 100f;
    private Image energyBar;
   

    void Start() {
        energyBar = transform.Find("EnergyCanvas/Energy").GetComponent<Image>();
    }

    void Update() {
        energyBar.fillAmount = energy / totalEnergy;
    }

    public void UseEnergy(float amount) {
        energy -= amount;
        energy = Mathf.Clamp(energy, 0, 100);
    }
}
