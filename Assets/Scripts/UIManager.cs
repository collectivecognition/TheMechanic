using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager> {
    private Animator currentUI;
    private int openParameter = Animator.StringToHash("Open");

    public static Dictionary<string, Animator> uis = new Dictionary<string, Animator>();

    void Start() {
        uis.Add("Dialogue", GameManager.Instance.cam.transform.parent.Find("UI/Dialogue").GetComponent<Animator>());
        uis.Add("NameEntry", GameManager.Instance.cam.transform.parent.Find("UI/NameEntry").GetComponent<Animator>());

        // Disable all UIs at startup

        foreach (KeyValuePair<string, Animator> entry in uis) {
            entry.Value.gameObject.SetActive(false);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            OpenUI(uis["NameEntry"]);
        }
    }

    public void OpenUI(Animator anim) {
        if(currentUI == anim) {
            return;
        }

        CloseCurrentUI();

        currentUI = anim;
        currentUI.gameObject.SetActive(true);
        currentUI.SetBool(openParameter, true);

        GameManager.instance.gameActive = false;
    }

    public void CloseCurrentUI() {
        if(currentUI == null) {
            return;
        }

        currentUI.SetBool(openParameter, false);

        StartCoroutine(DisablePanelDeleyed(currentUI));

        currentUI = null;

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
