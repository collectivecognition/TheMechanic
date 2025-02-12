﻿using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour {
    public float fadeSpeed = 2f;
    public float highIntensity = 2f;
    public float lowIntensity = 0.5f;
    public float changeMargin = 0.2f;
    public bool alarmOn = false;

    private float targetIntensity;
    private Light light;
    
    void Awake() {
        GetComponent<Light>().intensity = 0f;
        targetIntensity = highIntensity;

        light = GetComponent<Light>();
    }

    void Update() {
        if (alarmOn) {
            light.intensity = Mathf.Lerp(light.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            CheckTargetIntensity();
        }else {
            light.intensity = Mathf.Lerp(light.intensity, 0f, fadeSpeed * Time.deltaTime);
        }
    }

    void CheckTargetIntensity() {
        if(Mathf.Abs(targetIntensity - light.intensity) < changeMargin) {
            if(targetIntensity == highIntensity) {
                targetIntensity = lowIntensity;
            }else {
                targetIntensity = highIntensity;
            }
        }
    }
}
