//using UnityEngine;
//using System.Collections.Generic;

//public class DebugGUI : Singleton<DebugGUI> {
//    static string log;
//    private string output = "";
//    private string stack = "";
    
//    void OnEnable() {
//        Application.RegisterLogCallback(HandleLog);
//    }

//    void OnDisable() {
//        Application.RegisterLogCallback(null);
//    }

//    void HandleLog(string logString, string stackTrace, LogType type) {
//        output = logString;
//        stack = stackTrace;
//        log += "\n" + output;
//    }
   
//    void OnGUI() {
//        log = GUI.TextArea(new Rect(10, 10, Screen.width - 10, Screen.height - 10), log);
//        GUI.skin.textArea.normal.background = null;
//        GUI.skin.textArea.active.background = null;
//        GUI.skin.textArea.hover.background = null;
//    }
//}