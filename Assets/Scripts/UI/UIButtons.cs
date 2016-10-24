using UnityEngine;
using System.Collections;

public class UIButtons : MonoBehaviour {
    public static bool up;
    public static bool down;
    public static bool left;
    public static bool right;
    public static bool action;

    private float repeatRate = 0.1f;
    private float repeatRateHeld = 0.35f;

    private float lastUpTime = 0;
    private float lastDownTime = 0;
    private float lastLeftTime = 0;
    private float lastRightTime = 0;

    private bool upHeld = false;
    private bool downHeld = false;
    private bool leftHeld = false;
    private bool rightHeld = false;
    private bool actionHeld = false;

    void Update() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(x == 0) {
            leftHeld = false;
            rightHeld = false;
        }

        if(x == -1) { // Left
            if(leftHeld == false) {
                if (Time.fixedTime - lastLeftTime >= repeatRate) {
                    lastLeftTime = Time.fixedTime;
                    left = true;
                } else {
                    left = false;
                }
            }else {
                if(Time.fixedTime - lastLeftTime >= repeatRateHeld) {
                    lastLeftTime = Time.fixedTime;
                    left = true;
                } else {
                    left = false;
                }
            }

            leftHeld = true;
        }

        if (x == 1) { // Right
            if (rightHeld == false) {
                if (Time.fixedTime - lastRightTime >= repeatRate) {
                    lastRightTime = Time.fixedTime;
                    right = true;
                } else {
                    right = false;
                }
            } else {
                if (Time.fixedTime - lastRightTime >= repeatRateHeld) {
                    lastRightTime = Time.fixedTime;
                    right = true;
                } else {
                    right = false;
                }
            }

            rightHeld = true;
        }

        if (y == 0) {
            downHeld = false;
            upHeld = false;
        }

        if (y == -1) { // Down
            if (downHeld == false) {
                if (Time.fixedTime - lastDownTime >= repeatRate) {
                    lastDownTime = Time.fixedTime;
                    down = true;
                } else {
                    down = false;
                }
            } else {
                if (Time.fixedTime - lastDownTime >= repeatRateHeld) {
                    lastDownTime = Time.fixedTime;
                    down = true;
                } else {
                    down = false;
                }
            }

            downHeld = true;
        }

        if (y == 1) { // Up
            if (upHeld == false) {
                if (Time.fixedTime - lastUpTime >= repeatRate) {
                    lastUpTime = Time.fixedTime;
                    up = true;
                } else {
                    up = false;
                }
            } else {
                if (Time.fixedTime - lastUpTime >= repeatRateHeld) {
                    lastUpTime = Time.fixedTime;
                    up = true;
                } else {
                    up = false;
                }
            }

            upHeld = true;
        }

        if(Input.GetAxisRaw("Action") != 0 && !actionHeld) {
            action = true;
            actionHeld = true;
        }else {
            action = false;
        }

        if(Input.GetAxisRaw("Action") == 0) {
            actionHeld = false;
            action = false;
        }
    }
}
