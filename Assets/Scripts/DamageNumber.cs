using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageNumber : MonoBehaviour {
    private Text text;
    private CanvasGroup canvas;
    private Outline outline;

    void Start() {
        canvas = GetComponent<CanvasGroup>();
        outline = GetComponentInChildren<Outline>();

        StartCoroutine(Animate());
        Vector2 xy = Random.insideUnitCircle * 10f;
        transform.position += new Vector3(xy.x, 0f, xy.y);
    }

    IEnumerator Animate() {
        while (canvas.alpha > 0) {
            canvas.alpha -= Time.deltaTime / 2f;
            outline.effectColor = new Color(0f, 0f, 0f, canvas.alpha);
            transform.position += transform.up * Time.deltaTime * 10f;
            yield return null;
        }
        GameObject.Destroy(gameObject);
        yield return null;
    }

    public void Init(float value) {
        text = transform.Find("Text").GetComponent<Text>();
        text.text = value.ToString();
    }
}
