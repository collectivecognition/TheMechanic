﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager> {
    private Animator currentUI;
    private Action currentCallback;
    private int openParameter = Animator.StringToHash("Open");

    public Dictionary<string, Animator> uis = new Dictionary<string, Animator>();

    void Start() {
        Instance.uis.Add("Dialogue", GameManager.Instance.cam.transform.parent.Find("UI/Dialogue").GetComponent<Animator>());
        Instance.uis.Add("Inventory", GameManager.Instance.cam.transform.parent.Find("UI/Inventory").GetComponent<Animator>());
        Instance.uis.Add("NameEntry", GameManager.Instance.cam.transform.parent.Find("UI/NameEntry").GetComponent<Animator>());
        Instance.uis.Add("PostBattle", GameManager.Instance.cam.transform.parent.Find("UI/PostBattle").GetComponent<Animator>());

        // Disable all UIs at startup

        foreach (KeyValuePair<string, Animator> entry in uis) {
            entry.Value.gameObject.SetActive(false);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            UIManager.Instance.OpenUI(UIManager.Instance.uis["Inventory"]);
        }
    }

    public void OpenUI(Animator anim, Action callback=null) {
        if(currentUI == anim) {
            return;
        }

        CloseCurrentUI();

        currentUI = anim;
        currentCallback = callback;
        currentUI.gameObject.SetActive(true);
        currentUI.SetBool(openParameter, true);

        GameManager.instance.gameActive = false;
    }

    public void CloseCurrentUI() {
        if(currentUI == null) {
            return;
        }


        if (currentCallback != null) {
            currentCallback();
        }

        currentUI.SetBool(openParameter, false);
        StartCoroutine(DisablePanelDeleyed(currentUI));
        currentUI = null;
        currentCallback = null;
        GameManager.instance.gameActive = true;
    }

    IEnumerator DisablePanelDeleyed(Animator anim) {
        bool closedStateReached = false;
        bool wantToClose = true;
        while (!closedStateReached && wantToClose) {
            if (!anim.IsInTransition(0))
                closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName("Closed");

            wantToClose = !anim.GetBool(openParameter);

            yield return new WaitForEndOfFrame();
        }

        if (wantToClose) { 
            anim.gameObject.SetActive(false);
        }
    }
}
