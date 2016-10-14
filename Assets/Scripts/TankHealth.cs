using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankHealth : MonoBehaviour {
    private float health = 100f;
    private float totalHealth = 100f;
    private Image healthBar;

    public delegate void OnDieEvent(GameObject gameObject);
    public event OnDieEvent OnDie;

    void Start() {
        healthBar = transform.Find("HealthCanvas/Health").GetComponent<Image>();
    }

    void Update() {
        healthBar.fillAmount = health / totalHealth;
    }

    public void Hit(float damage) {
        health -= damage;
        if (health <= 0f) {
            // GameObject.Destroy(gameObject);
            OnDie(gameObject);
        }
    }
}
