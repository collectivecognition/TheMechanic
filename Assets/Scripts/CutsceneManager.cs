using UnityEngine;
using System.Collections;
using System;

public class CutsceneManager : Singleton<CutsceneManager> {
    private CutsceneEvent[] cutscene;
    private int currentEventIndex;

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
