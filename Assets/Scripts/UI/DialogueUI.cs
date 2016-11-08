using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class DialogueUI : MonoBehaviour {
    private Text textObject;
    private GameObject canvasObject;
    private AudioSource audio;
    private AudioClip sound;

    private int charsPerLine = 19;
    private string text;
    private bool done = false;
    private Action callback;

    void Awake() {
        textObject = transform.Find("Dialogue/Canvas/Text").GetComponent<Text>();
        audio = transform.Find("Dialogue").GetComponent<AudioSource>();
        sound = Resources.Load<AudioClip>("Sounds/TextBeep");

        canvasObject = gameObject;

        GameManager.Instance.dialogueUI = this;
    }

    public void Say(string what, Action cb = null) {
        callback = cb;
        text = what;
        done = false;

        // Inject line breaks into text

        string[] words = what.Split(' ');
        string formattedText = "";
        string currentLine = "";

        for(int ii = 0; ii < words.Length; ii++) {
            string word = words[ii];

            if(currentLine.Length + word.Length <= charsPerLine) {
                currentLine += currentLine.Length > 0 ? " " + word : word;

                if(currentLine.Substring(currentLine.Length - 1, 1) == "\n") {
                    formattedText += currentLine;
                    currentLine = "";
                }
            } else {
                formattedText += currentLine + "\n";
                currentLine = word;
            }

            if(ii == words.Length - 1) {
                formattedText += currentLine;
            }
        }

        text = formattedText;

        // Zoom in

        UIManager.Instance.OpenUI("Dialogue");
        textObject.text = "";
        StartCoroutine(TypeText(text));
    }

    public void Choose(string what, string[] choices, Action<int> cb) {
        // TODO
    }

    void Update() {
        if(!UIManager.Instance.IsOpen("Dialogue")) { return; }

        // Finishing dialogue

        if (done && UIButtons.action) {
            UIManager.Instance.CloseUI("Dialogue");

            if (callback != null) {
                callback();
            }
        } else {

            // Rush text

            if (!done && UIButtons.action) {
                done = true;
                textObject.text = text;
            }
        }
    }

    IEnumerator TypeText(string what) {
        foreach (char letter in what.ToCharArray()) {
            if (done) {
                break;
            }

            textObject.text += letter;
            if (char.IsLetter(letter)) {
                audio.PlayOneShot(sound);
                yield return new WaitForSeconds(0.02f);
            } else {
                yield return new WaitForSeconds(0.1f);
            }
        }

        done = true;
    }
}
