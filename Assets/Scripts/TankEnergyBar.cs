using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankEnergyBar : MonoBehaviour {
    private Energy energy;
    private Image energyBar;
    private GameObject energyCanvas;

    void Start() {
        energyBar = transform.Find("EnergyCanvas/Energy").GetComponent<Image>();
        energyCanvas = transform.Find("EnergyCanvas").gameObject;
        energy = PlayerManager.Instance.energy;
    }

    void Update() {
        energyBar.fillAmount = energy.current / energy.total;

        energyCanvas.SetActive(BattleManager.Instance.BattleActive); // Only show in battle mode
    }
}
