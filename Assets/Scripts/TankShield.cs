using UnityEngine;
using System.Collections;

public class TankShield : MonoBehaviour {
    private float animateTime = 0f;
    private float animateSpeed = 4f;
    private float energyUsePerSecond = 25f;
    private bool enabled;
    private bool charged = true;

    private Material material;
    private Energy energy;
    private AudioSource audioSource;
    private AudioClip audioClip;

    void Awake() {
        material = transform.Find("Shield").GetComponent<Renderer>().material;
        energy = PlayerManager.Instance.energy;
        audioSource = transform.parent.GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Sounds/Shield");

        Disable();
    }

    void Update() {
        if (!GameManager.Instance.gameActive) return;

        // Animate shield

        animateTime += Time.deltaTime * animateSpeed;
        material.SetFloat("_Offset", Mathf.Repeat(animateTime, 1f));

        if (energy.current < 1f || !BattleManager.Instance.BattleActive) {
            Disable();
            charged = false;
        } else {
            if (Input.GetAxisRaw("Action") != 0 && charged) {
                Enable();
                energy.Use(energyUsePerSecond * Time.deltaTime);
            }

            if (Input.GetAxisRaw("Action") == 0) {
                Disable();

                if(energy.current > 1f) {
                    charged = true;
                }
            }
        }
    }

    void Enable() {
        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.one, "time", 1f));

        if (!enabled) {
            audioSource.PlayOneShot(audioClip);
        }
        enabled = true;
    }

    void Disable() {
        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f));
        enabled = false;
    }
}