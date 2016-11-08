using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PostBattleUI : MonoBehaviour {
    private Text textObject;

    void Start() {
        textObject = transform.Find("PostBattle/Canvas/Text").GetComponent<Text>();
        GameManager.Instance.postBattleUI = this;
    }

    void Update() {
        if(!UIManager.Instance.IsOpen("PostBattle")) { return; }

        if(UIButtons.back) {
            UIManager.Instance.CloseUI("PostBattle");
        }
    }

    public void Open(int exp, InventoryItem[] loot, Action callback=null) {
        UIManager.Instance.OpenUI("PostBattle");

        textObject.text =  "BATTLE COMPLETE\n\n";
        textObject.text += "You got:\n";
        textObject.text += exp + " exp\n";

        foreach(InventoryItem i in loot) {
            textObject.text += "1x " + i.name + "\n";
        }

        UIManager.Instance.OpenUI("PostBattle", true, callback);
    }
}
