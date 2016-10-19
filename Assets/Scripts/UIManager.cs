using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : Singleton<UIManager> {
    private Animator currentUI;
    private int openParameter = Animator.StringToHash("Open");

    void Awake() {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            Animator testAnim = GameManager.Instance.cam.transform.parent.Find("TestUI").GetComponent<Animator>();
            OpenUI(testAnim);
        }

        if (Input.GetKeyDown(KeyCode.N)) {
            CloseCurrentUI();
        }
    }

    public void OpenUI(Animator anim) {
        if(currentUI == anim) {
            return;
        }

        CloseCurrentUI();

        currentUI = anim;
        currentUI.SetBool(openParameter, true);
        currentUI.gameObject.SetActive(true);
    }

    public void CloseCurrentUI() {
        if(currentUI == null) {
            return;
        }

        currentUI.SetBool(openParameter, false);

        StartCoroutine(DisablePanelDeleyed(currentUI));

        currentUI = null;
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
