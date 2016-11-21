using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EditableCube))]
[CanEditMultipleObjects]
public class EditableCubeEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        EditorGUILayout.LabelField("Snap", EditorStyles.boldLabel);
        if(GUILayout.Button("Snap")) {
            foreach(Object obj in targets) {
                ((EditableCube)obj).Snap();
            }
        }


        EditorGUILayout.LabelField("Move", EditorStyles.boldLabel);
        
        GUILayout.BeginHorizontal();
        GUILayout.Button("", GUILayout.Width(100f));
        if(GUILayout.Button("Forward", GUILayout.Width(100f))) {
            foreach(Object obj in targets) {
                ((EditableCube)obj).Move(Vector3.forward * Grid.size);
            }
        }
        if(GUILayout.Button("Up", GUILayout.Width(100f))) {
            foreach(Object obj in targets) {
                ((EditableCube)obj).Move(Vector3.up * Grid.size);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Left", GUILayout.Width(100f))) {
            foreach(Object obj in targets) {
                ((EditableCube)obj).Move(Vector3.left * Grid.size);
            }
        }
        GUILayout.Button("", GUILayout.Width(100f));
        if(GUILayout.Button("Right", GUILayout.Width(100f))) {
            foreach(Object obj in targets) {
                ((EditableCube)obj).Move(Vector3.right * Grid.size);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Button("", GUILayout.Width(100f));
        if(GUILayout.Button("Back", GUILayout.Width(100f))) {
            foreach(Object obj in targets) {
                ((EditableCube)obj).Move(Vector3.back * Grid.size);
            }
        }
        if(GUILayout.Button("Down", GUILayout.Width(100f))) {
            foreach(Object obj in targets) {
                ((EditableCube)obj).Move(Vector3.down * Grid.size);
            }
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Duplicate", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        GUILayout.Button("", GUILayout.Width(100f));
        if(GUILayout.Button("Forward", GUILayout.Width(100f))) {
            foreach(Object obj in targets) {
                ((EditableCube)obj).Duplicate(Vector3.forward * Grid.size);
            }
        }
        if(GUILayout.Button("Up", GUILayout.Width(100f))) {
            foreach(Object obj in targets) {
                ((EditableCube)obj).Duplicate(Vector3.up * Grid.size);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Left", GUILayout.Width(100f))) {
            foreach(Object obj in targets) {
                ((EditableCube)obj).Duplicate(Vector3.left * Grid.size);
            }
        }
        GUILayout.Button("", GUILayout.Width(100f));
        if(GUILayout.Button("Right", GUILayout.Width(100f))) {
            foreach(Object obj in targets) {
                ((EditableCube)obj).Duplicate(Vector3.right * Grid.size);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Button("", GUILayout.Width(100f));
        if(GUILayout.Button("Back", GUILayout.Width(100f))) {
            foreach(Object obj in targets) {
                ((EditableCube)obj).Duplicate(Vector3.back * Grid.size);
            }
        }
        if(GUILayout.Button("Down", GUILayout.Width(100f))) {
            foreach(Object obj in targets) {
                ((EditableCube)obj).Duplicate(Vector3.down * Grid.size);
            }
        }
        GUILayout.EndHorizontal();
    }
}