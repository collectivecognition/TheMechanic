using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NameEntry : MonoBehaviour {
    private string[,] buttons = new string[,] {
        { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" },
        { "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" },
        { "U", "V", "W", "X", "Y", "Z", "Back", "Back", "Done", "Done" }
    };
    private string text = "BILL";
    private int currentRow = 2;
    private int currentCol = 9;
    private float lastMove;
    private float moveDelay = 0.3f;
    private Text nameText;
    private GameObject player;

    void Start() {
        nameText = transform.Find("NameText").GetComponent<Text>();
        player = GameObject.Find("Shared/Player");

        GameManager.Instance.gameActive = false;
    }
    
	void Update () {
        nameText.text = text;

        transform.Find(buttons[currentRow, currentCol]).GetComponent<Text>().color = Color.white;

        string currentButton = buttons[currentRow, currentCol];

        if (Input.GetKeyDown(KeyCode.Space)) {
            switch (currentButton) {
                case "Done":
                    gameObject.active = false;
                    GameManager.Instance.playerName = text;
                    GameManager.Instance.gameActive = true;
                    GameManager.Instance.LoadScene("Desert", "SpawnPoint");
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

        

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x == 0 && y == 0) {
            lastMove = Time.deltaTime - moveDelay;
        }

        if (Time.fixedTime - lastMove >= moveDelay) {
            if (x > 0) {
                if (currentCol < 9) {
                    if(currentButton == "Back") {
                        currentCol = 8;
                    }else {
                        currentCol++;
                    }
                    lastMove = Time.fixedTime;
                }
            }

            if (x < 0) {
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
                    lastMove = Time.fixedTime;
                }
            }

            if (y < 0) {
                if (currentRow < 2) {
                    currentRow++;
                    lastMove = Time.fixedTime;
                }
            }

            if (y > 0) {
                if (currentRow > 0) {
                    currentRow--;
                    lastMove = Time.fixedTime;
                }
            }
        }

        currentButton = buttons[currentRow, currentCol];

        transform.Find(currentButton).GetComponent<Text>().color = Color.yellow;

    }
}
