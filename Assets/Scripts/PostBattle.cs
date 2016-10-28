using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PostBattleManager : Singleton<PostBattleManager> {
    private Text textObject;

    void Start() {
        textObject = GameManager.Instance.cameraObject.transform.Find("UI/PostBattle/Canvas/Text").GetComponent<Text>();
    }

    void Update() {
        if(Input.GetAxisRaw("Action") != 0) {
            UIManager.Instance.CloseCurrentUI();
        }
    }

    public void Do(int exp, InventoryItem[] loot, Action callback=null) {
        textObject.text =  "BATTLE COMPLETE\n\n";
        textObject.text += "You got:\n";
        textObject.text += exp + " exp\n";

        foreach(InventoryItem i in loot) {
            textObject.text += "1x " + i.name + "\n";
        }

        UIManager.Instance.OpenUI(UIManager.Instance.uis["PostBattle"], callback);
    }
}
