using UnityEngine;
using System.Collections;

public class TankShield : MonoBehaviour {
    private float animateTime = 0f;
    private float animateSpeed = 4f;
    private float energyUsePerSecond = 25f;

    private Material material;
    private Energy energy;

    void Awake() {
        material = transform.Find("Shield").GetComponent<Renderer>().material;
        energy = PlayerManager.Instance.energy;

        Disable();
    }

    void Update() {
        if (!GameManager.Instance.gameActive) return;

        // Animate shield

        animateTime += Time.deltaTime * animateSpeed;
        material.SetFloat("_Offset", Mathf.Repeat(animateTime, 1f));

        if (energy.current < 1f || !BattleManager.Instance.BattleActive) {
            Disable();
        } else {
            if (Input.GetAxisRaw("Action") != 0) {
                Enable();
                energy.Use(energyUsePerSecond * Time.deltaTime);
            }

            if (Input.GetAxisRaw("Action") == 0) {
                Disable();
            }
        }
    }

    void Enable() {
        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.one, "time", 1f));
    }

    void Disable() {
        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f));
    }
}