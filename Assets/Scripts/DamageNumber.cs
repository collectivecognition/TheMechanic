using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageNumber : MonoBehaviour {
    private Text text;
    private CanvasGroup canvas;

    void Start() {
        canvas = GetComponent<CanvasGroup>();
        StartCoroutine(Animate());
    }

    IEnumerator Animate() {
        while (canvas.alpha > 0) {
            canvas.alpha -= Time.deltaTime / 2f;
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
