using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NameEntryUI : MonoBehaviour {
    private string[,] buttons = new string[,] {
        { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" },
        { "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" },
        { "U", "V", "W", "X", "Y", "Z", "Back", "Back", "Done", "Done" }
    };
    private string text = "BILL";
    private int currentRow = 2;
    private int currentCol = 9;
    private Text nameText;

    void Awake() {
        nameText = transform.Find("Canvas/NameText").GetComponent<Text>();
    }
    
	void Update () {
        nameText.text = text;

        transform.Find("Canvas/Buttons/" + buttons[currentRow, currentCol]).GetComponent<Text>().color = Color.white;

        string currentButton = buttons[currentRow, currentCol];

        if (Input.GetKeyDown(KeyCode.Space)) {
            switch (currentButton) {
                case "Done":
                    GameManager.Instance.playerName = text;
                    UIManager.Instance.CloseUI("NameEntry");
                    break;

                case "Back":
                    if(text.Length > 0) {
                        text = text.Substring(0, text.Length - 1);
                    }
                    break;

                default:
                    if (text.Length < 4) {
                        text += currentButton;
                    }
                    break;
            }
        }

        if (UIButtons.right) {
            if (currentCol < 9) {
                if(currentButton == "Back") {
                    currentCol = 8;
                }else {
                    currentCol++;
                }
            }
        }

        if (UIButtons.left) {
            if (currentCol > 0) {
                switch (currentButton) {
                    case "Done":
                        currentCol = 7;
                        break;
                    case "Back":
                        currentCol = 5;
                        break;
                    default:
                        currentCol--;
                        break;
                }
            }
        }

        if (UIButtons.down) {
            if (currentRow < 2) {
                currentRow++;
            }
        }

        if (UIButtons.up) {
            if (currentRow > 0) {
                currentRow--;
            }
        }

        currentButton = buttons[currentRow, currentCol];

        transform.Find("Canvas/Buttons/" + currentButton).GetComponent<Text>().color = Color.yellow;
    }
}
