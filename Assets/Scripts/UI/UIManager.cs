using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager> {
    private Animator currentUI;
    private Action currentCallback;

    private int openParameter = Animator.StringToHash("Open");
    private Dictionary<string, Animator> animators = new Dictionary<string, Animator>();
    private Dictionary<string, Action> callbacks = new Dictionary<string, Action>();
    private Dictionary<string, bool> pauseStatus = new Dictionary<string, bool>();
    private Dictionary<string, bool> openStatus = new Dictionary<string, bool>();

    void Start() {
        animators.Add("Dialogue", GameManager.Instance.cameraObject.transform.Find("UI/Dialogue").GetComponent<Animator>());
        animators.Add("Inventory", GameManager.Instance.cameraObject.transform.Find("UI/Inventory").GetComponent<Animator>());
        animators.Add("NameEntry", GameManager.Instance.cameraObject.transform.Find("UI/NameEntry").GetComponent<Animator>());
        animators.Add("PostBattle", GameManager.Instance.cameraObject.transform.Find("UI/PostBattle").GetComponent<Animator>());
        animators.Add("Computer", GameManager.Instance.cameraObject.transform.Find("UI/Computer").GetComponent<Animator>());
        animators.Add("Notification", GameManager.Instance.cameraObject.transform.Find("UI/Notification").GetComponent<Animator>());

        // Disable all UIs at startup and init callback dict

        foreach (KeyValuePair<string, Animator> entry in animators) {
            entry.Value.gameObject.SetActive(false);
            callbacks.Add(entry.Key, null);
            openStatus[entry.Key] = false;
        }
    }
   
    public void OpenUI(string name, bool pause=true, Action callback=null) {
        Animator anim = animators[name];

        callbacks[name] = callback;
        openStatus[name] = true;
        anim.gameObject.SetActive(true);
        anim.SetBool(openParameter, true);

        if(pause){
            GameManager.Instance.gameActive = false;
        }
        pauseStatus[name] = pause;
    }

    public void CloseUI(string name) {
        Animator anim = animators[name];

        Debug.Log("Close UI: " + name);

        anim.SetBool(openParameter, false);
        StartCoroutine(DisablePanelDeleyed(name));
        openStatus[name] = false;

        // If no UI is open which requires the game to be paused

        bool pause = true;
        foreach(KeyValuePair<string, bool> entry in pauseStatus) {
            if(openStatus[entry.Key] && entry.Value) {
                pause = false;
            }
        }
        GameManager.Instance.gameActive = pause;
    }
    
    public bool IsOpen(string name) {
        return openStatus[name];
    }

    IEnumerator DisablePanelDeleyed(string name) {
        Animator anim = animators[name];
        bool closedStateReached = false;
        //bool wantToClose = true;
        while (!closedStateReached) {
            if (!anim.IsInTransition(0))
                closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName("Closed");

            //wantToClose = !anim.GetBool(openParameter);

            yield return new WaitForEndOfFrame();
        }
        //if (wantToClose) { 
            anim.gameObject.SetActive(false);

            if (callbacks[name] != null) {
                callbacks[name]();
            }
            callbacks[name] = null;
        //}
    }
}
