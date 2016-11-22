using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageNumber : MonoBehaviour {
    private Text text = null;
    private CanvasGroup canvas;
    private Outline outline;

    public float damage = 0;

    void Start() {
        canvas = GetComponent<CanvasGroup>();
        outline = GetComponentInChildren<Outline>();

        StartCoroutine(Animate());
        //Vector2 xy = Random.insideUnitCircle * 10f;
        //transform.position += new Vector3(xy.x, 0f, xy.y);
    }

    IEnumerator Animate() {
        while (canvas.alpha > 0) {
            canvas.alpha -= Time.deltaTime * 1f;
            outline.effectColor = new Color(0f, 0f, 0f, canvas.alpha);
            transform.position += transform.up * Time.deltaTime * 10f;
            yield return null;
        }
        GameObject.Destroy(gameObject);
        yield return null;
    }

    public void AddDamage(float d) {
        if(text == null) {
            text = GetComponentInChildren<Text>();
        }
        damage += d;
        text.text = damage.ToString();
        canvas.alpha = 1f;
    }
}
