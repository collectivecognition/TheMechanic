using UnityEngine;
using System.Collections;
using System;

public class CutsceneManager : Singleton<CutsceneManager> {
    public Dialogue dialogue;

    private CutsceneEvent[] cutscene;
    private int currentEventIndex;

    void Start() {
        dialogue = GameObject.Find("Shared/Camera/Dialogue").GetComponent<Dialogue>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            CutsceneEvent[] c = new CutsceneEvent[] {
                new CutsceneDialogEvent("Teacher: Today we’ll be learning about the great Human War, in which our people definitively crushed our human oppressors and ushered 500 years of glorious, perpetual war."),
                new CutsceneDialogEvent(GameManager.Instance.playerName + ", please write the answer to A QUESTION on the BLACKBOARD")
            };

            Play(c);
        }
    }

    public void Play(CutsceneEvent[] c, Action callback = null) {
        GameManager.Instance.gameActive = false;

        cutscene = c;
        currentEventIndex = 0;
        NextEvent();
    }

    private void NextEvent() {
        cutscene[currentEventIndex].Play(() => {
            if (currentEventIndex < cutscene.Length - 1) {
                currentEventIndex++;
                NextEvent();
            } else {
                GameManager.Instance.gameActive = true;
            }
        });
    }
}
