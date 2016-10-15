using UnityEngine;
using System;
using System.Collections;

public class CutsceneDialogEvent : CutsceneEvent {
    private string text;
    private string[] choices;

    public CutsceneDialogEvent(string t, string[] c = null) {
        text = t;
        choices = c;
    }

    override public void Play(Action callback = null) {
        if (choices == null) {
            CutsceneManager.Instance.dialogue.Say(text, callback);
        } else {
            CutsceneManager.Instance.dialogue.Choose(text, choices, (int choice) => {
                if(callback != null) {

                }
            });
        }
    }
}
