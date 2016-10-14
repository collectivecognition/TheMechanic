using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankEnergy : MonoBehaviour {
    public float Energy { get { return energy;}  }

    private float energy = 100f;
    private float totalEnergy = 100f;
    private float rechargeRate = 15f;

    private Image energyBar;
    private GameObject energyCanvas;

    void Start() {
        energyBar = transform.Find("EnergyCanvas/Energy").GetComponent<Image>();
        energyCanvas = transform.Find("EnergyCanvas").gameObject;
    }

    void Update() {
        energyBar.fillAmount = energy / totalEnergy;

        energy += rechargeRate * Time.deltaTime;
        energy = Mathf.Clamp(energy, 0, 100);

        energyCanvas.SetActive(BattleManager.Instance.BattleActive); // Only show in battle mode
    }

    public void UseEnergy(float amount) {
        energy -= amount;
    }
}
