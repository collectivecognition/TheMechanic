using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Powerup : MonoBehaviour {
    public string inventoryItemName = null;

    private static Dictionary<string, float> lootTable = new Dictionary<string, float>() {
        { "HealthPotionItem", 50f },
        { "EnergyPotionItem", 50f }
    };
    private float lootTableWeightSum = lootTable.Sum(i => i.Value);
    private bool pickedUp = false;

	void Start (){
        iTween.RotateAdd(gameObject, iTween.Hash("y", 180f, "time", 1.5f, "loopType", iTween.LoopType.loop, "easeType", iTween.EaseType.linear));
	}

    private string RandomItem() {
        float r = Random.Range(0f, lootTableWeightSum);
        float t = 0f;

        foreach(KeyValuePair<string, float> item in lootTable) {
            t += item.Value;
            if(t >= r) {
                return item.Key;
            }
        }

        return null;
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == "Player" && !pickedUp) {
            pickedUp = true;

            if(inventoryItemName == null || inventoryItemName.Length == 0) {
                inventoryItemName = RandomItem();
            }

            GetComponent<ParticleSystem>().Play();
            GetComponent<Renderer>().enabled = false;
            GetComponent<Light>().intensity = 10f;
            GetComponent<Light>().range = 10f;
            GameObject.Destroy(gameObject, 0.3f);

            InventoryItem item = InventoryManager.Instance.inventory.AddItemByName(inventoryItemName);
            GameManager.Instance.notification.Notify("You got: " + item.name);
                
            
        }
    }
}
