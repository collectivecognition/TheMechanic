using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankDialog : MonoBehaviour {
    private Text textObject;
    private GameObject canvasObject;
    private GameObject player;
    private bool saying = false;
    private bool done = false;

    void Start() {
        textObject = transform.Find("DialogCanvas/Text").GetComponent<Text>();
        canvasObject = transform.Find("DialogCanvas").gameObject;
        player = GameObject.Find("Shared/Player");
    }

    public void Say(string what) {
        if (saying) {
            return;
        }

        saying = true;
        done = false;

        canvasObject.transform.localScale = Vector3.zero;
        iTween.ScaleTo(canvasObject, iTween.Hash("scale", new Vector3(1, 1, 1), "time", 0.5f));
        textObject.text = "";
        StartCoroutine(TypeText(what));

        GameManager.Instance.gameActive = false;
    }

    void Update () {
        if(saying && done && Input.GetKeyDown(KeyCode.Space)) {
            saying = false;
            iTween.ScaleTo(canvasObject, iTween.Hash("scale", Vector3.zero, "time", 0.5f));

            GameManager.Instance.gameActive = true;
        }
    }

    IEnumerator TypeText(string what) {
        foreach (char letter in what.ToCharArray()) {
            textObject.text += letter;
            //if (sound)
                //audio.PlayOneShot(sound);
            yield return 0;
            yield return new WaitForSeconds(0.02f);
        }

        done = true;
    }
}
