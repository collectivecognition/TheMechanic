using UnityEngine;
using System;
using System.Collections;

public class CutsceneDialogueEvent : CutsceneEvent {
    private string text;
    private string[] choices;

    public CutsceneDialogueEvent(string t, string[] c = null) {
        text = t;
        choices = c;
    }

    override public void Play(Action callback = null) {
        if (choices == null) {
            GameManager.Instance.dialogueUI.Say(text, callback);
        } else {
            GameManager.Instance.dialogueUI.Choose(text, choices, (int choice) => {
                if(callback != null) {

                }
            });
        }
    }
}
