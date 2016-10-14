using UnityEngine;
using System;
using System.Collections;

public class CutsceneDialogEvent : CutsceneEvent {
    private string text;

    public CutsceneDialogEvent(string t) {
        text = t;
    }

    override public void Play(Action callback=null) {
        CutsceneManager.Instance.dialogue.Say(text, callback);
    }
}
