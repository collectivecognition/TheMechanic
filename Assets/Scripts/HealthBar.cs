using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class HealthBar : MonoBehaviour {
    private Health health; // A reference to the health object, either a player's or an enemy's
    private GameObject healthCanvas;
    private Image healthBar;
    private GameObject damageNumberPrefab;
    private Renderer[] renderers;

    private bool blinking = false;
    private Color[] brighter;
    private Color[] darker;

    public delegate void OnDieEvent(GameObject gameObject);
    public event OnDieEvent OnDie;


    void Start() {
        healthCanvas = transform.Find("HealthCanvas").gameObject;
        healthBar = transform.Find("HealthCanvas/Health").GetComponent<Image>();
        damageNumberPrefab = Resources.Load<GameObject>("Prefabs/DamageNumber");
        renderers = gameObject.GetComponentsInChildren<Renderer>();

        darker = new Color[renderers.Length];
        brighter = new Color[renderers.Length];

        for(int ii = 0; ii < renderers.Length; ii++) {
            darker[ii] = renderers[ii].material.color;
            brighter[ii] = darker[ii] + new Color(1f, 1f, 1f);
        }

        if (tag == "Player") {
            health = PlayerManager.Instance.health;
        }else {
            health = new Health(GetComponent<Enemy>().totalHealth);
        }
    }

    void Update() {
        healthBar.fillAmount = health.current / health.total;

        healthCanvas.SetActive(BattleManager.Instance.BattleActive); // Only show in battle mode
    }

    public void Hit(float damage) {
        damage = Mathf.Ceil(damage);
        health.current -= damage;

        GameObject damageNumber = GameObject.Instantiate(damageNumberPrefab);
        damageNumberPrefab.GetComponent<DamageNumber>().Init(damage);
        damageNumber.transform.position = transform.position + Vector3.up * 5f;

        // DIE!!#!

        if (health.current <= 0f) {
            GameObject explosion = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Explosion"), null, true);
            explosion.transform.position = transform.position;
            explosion.GetComponent<Explosion>().color = darker[0]; // FIXME: Maybe make this configurable?

            GameObject loot = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Powerup"));
            loot.transform.position = transform.position;
            loot.GetComponent<Powerup>().inventoryItemName = "PeaShooter";
            GameObject.Destroy(gameObject);

            OnDie(gameObject);
        }

        int blinks = 2;
        float blinkSpeed = 0.075f;

        if (!blinking) {
            blinking = true;

            for (int ii = 0; ii < blinks * 2; ii++) {
                for (int jj = 0; jj < renderers.Length; jj++) {
                    iTween.ColorTo(renderers[jj].gameObject, iTween.Hash(
                        "color", (ii % 2 == 0 ? brighter[jj] : darker[jj]),
                        "time", blinkSpeed,
                        "delay", blinkSpeed * ii
                    ));
                }

            }
            StartCoroutine(OnDoneBlinking(blinks * 2 * blinkSpeed));
        }
    }

    IEnumerator OnDoneBlinking(float delay) {
        yield return new WaitForSeconds(delay); 
        blinking = false;
    }
}
