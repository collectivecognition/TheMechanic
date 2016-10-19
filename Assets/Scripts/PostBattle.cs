using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PostBattleManager : Singleton<PostBattleManager> {
    private Text textObject;

    void Start() {
        textObject = GameManager.Instance.cam.transform.parent.Find("UI/PostBattle/Canvas/Text").GetComponent<Text>();
    }

    public void Do(int exp, params InventoryItem[] loot) {
        textObject.text =  "BATTLE COMPLETE\n\n";
        textObject.text += "You got:\n";
        textObject.text += exp + " exp\n";

        foreach(InventoryItem i in loot) {
            textObject.text += "1x " + i.name + "\n";
        }

        UIManager.Instance.OpenUI(UIManager.Instance.uis["PostBattle"]);
    }
}
