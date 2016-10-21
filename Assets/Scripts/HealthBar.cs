using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
    private Health health; // A reference to the health object, either a player's or an enemy's

    private GameObject healthCanvas;
    private Image healthBar;
    private GameObject damageNumberPrefab;

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
            GameObject.Destroy(gameObject);
            OnDie(gameObject);
        }

        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0f, "time", 0.1f));
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 1f, "time", 0.1f, "delay", 0.1));
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0f, "time", 0.1f, "delay", 0.2));
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 1f, "time", 0.1f, "delay", 0.3));
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0f, "time", 0.1f, "delay", 0.4));
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 1f, "time", 0.1f, "delay", 0.5));
    }
}
