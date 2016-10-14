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
            CutsceneEvent[] cutscene = new CutsceneEvent[] {
                new CutsceneDialogEvent("Lorem ipsum.."),
                new CutsceneDialogEvent("Dorem amet...")
            };

            cutscene[0].Play(() => {
                cutscene[1].Play(() => {
                    GameManager.Instance.gameActive = true;
                });
            });
        }
    }

    public void Play(CutsceneEvent[] cutscene, Action callback = null) {
        GameManager.Instance.gameActive = false;

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
