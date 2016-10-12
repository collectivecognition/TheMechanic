using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankDialog : MonoBehaviour {
    Text textObject;
    GameObject canvasObject;

    void Start() {
        textObject = transform.Find("DialogCanvas/Text").GetComponent<Text>();
        canvasObject = transform.Find("DialogCanvas").gameObject;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            Say("You grabbed her by the WHAT?!");
        }
    }

    public void Say(string what) {
        canvasObject.transform.localScale = Vector3.zero;
        iTween.ScaleTo(canvasObject, iTween.Hash("scale", new Vector3(1, 1, 1), "time", 0.5f));
        textObject.text = "";
        StartCoroutine(TypeText(what));
    }

    IEnumerator TypeText(string what) {
        foreach (char letter in what.ToCharArray()) {
            textObject.text += letter;
            //if (sound)
                //audio.PlayOneShot(sound);
            yield return 0;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
