﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Dialogue : MonoBehaviour {
    private Text textObject;
    private GameObject canvasObject;
    private AudioSource audio;
    private AudioClip sound;

    private string text;
    private bool done = false;
    private Action callback;

    void Start() {
        textObject = transform.Find("Text").GetComponent<Text>();
        audio = GetComponent<AudioSource>();
        sound = Resources.Load<AudioClip>("Sounds/TextBeep");

        canvasObject = gameObject;
    }

    public void Say(string what, Action cb=null) {
        callback = cb;
        text = what;
        done = false;

        // Zoom in
        // canvasObject.transform.localScale = Vector3.zero;
        iTween.ScaleTo(canvasObject, iTween.Hash("scale", new Vector3(1, 1, 1), "time", 0.5f));
        textObject.text = "";
        StartCoroutine(TypeText(what));
    }

    public void Choose(string what, string[] choices, Action<int> cb) {

    }

    void Update() {

        // Finishg dialogue

        if (done && Input.GetKeyDown(KeyCode.Space)) {
            iTween.ScaleTo(canvasObject, iTween.Hash("scale", Vector3.zero, "time", 0.5f));
            if(callback != null) {
                callback();
            }
        } else {

            // Rush text

            if (!done && Input.GetKeyDown(KeyCode.Space)) {
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
