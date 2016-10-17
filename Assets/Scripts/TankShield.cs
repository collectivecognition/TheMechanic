using UnityEngine;
using System.Collections;

public class TankShield : MonoBehaviour {
    private float animateTime = 0f;
    private float animateSpeed = 4f;
    private float energyUsePerSecond = 50f;

    private Material material;
    private TankEnergy energy;

    void Awake() {
        material = transform.Find("Shield").GetComponent<Renderer>().material;
        energy = transform.GetComponentInParent<TankEnergy>();

        Disable();
    }

    void Update() {

        // Animate shield

        animateTime += Time.deltaTime * animateSpeed;
        material.SetFloat("_Offset", Mathf.Repeat(animateTime, 1f));

        if (energy.Energy < 1f) {
            Disable();
        } else {
            if (Input.GetMouseButtonDown(1)) {
                Enable();
            }

            if (Input.GetMouseButtonUp(1)) {
                Disable();
            }

            if (Input.GetMouseButton(1)) {
                energy.UseEnergy(energyUsePerSecond * Time.deltaTime);
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