using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class HealthBar : MonoBehaviour {
    private Health health; // A reference to the health object, either a player's or an enemy's
    private GameObject healthCanvas;
    private Image healthBar;
    private GameObject damageNumberPrefab;

    private bool blinking = false;
    private Color brighter;
    private Color darker;

    public delegate void OnDieEvent(GameObject gameObject);
    public event OnDieEvent OnDie;


    void Start() {
        healthCanvas = transform.Find("HealthCanvas").gameObject;
        healthBar = transform.Find("HealthCanvas/Health").GetComponent<Image>();
        damageNumberPrefab = Resources.Load<GameObject>("Prefabs/DamageNumber");

        if(tag == "Player") {
            health = PlayerManager.Instance.health;
        }else {
            health = new Health(50f);
        }

        darker = gameObject.GetComponentInChildren<Renderer>().material.color;
        brighter = darker + new Color(2f, 2f, 2f);
    }

    void Update() {
        healthBar.fillAmount = health.current / health.total;

        healthCanvas.SetActive(BattleManager.Instance.BattleActive); // Only show in battle mode
    }

    public void Hit(float damage) {
        health.current -= damage;

        GameObject damageNumber = GameObject.Instantiate(damageNumberPrefab);
        damageNumberPrefab.GetComponent<DamageNumber>().Init(damage);
        damageNumber.transform.position = transform.position + Vector3.up * 5f;

        if (health.current <= 0f) {
            GameObject explosion = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Explosion"), null, true);
            explosion.transform.position = transform.position;
            explosion.GetComponent<Explosion>().color = darker;
            GameObject.Destroy(gameObject);
            OnDie(gameObject);
        }

        int blinks = 2;
        float blinkSpeed = 0.075f;

        if (!blinking) {
            blinking = true;

            for (int ii = 0; ii < blinks * 2; ii++) {
                iTween.ColorTo(gameObject, iTween.Hash(
                    "color", (ii % 2 == 0 ? brighter : darker),
                    "time", blinkSpeed, 
                    "delay", blinkSpeed * ii
                ));
            }
            StartCoroutine(OnDoneBlinking(blinks * 2 * blinkSpeed));
        }
    }

    IEnumerator OnDoneBlinking(float delay) {
        yield return new WaitForSeconds(delay); 
        blinking = false;
    }
}
