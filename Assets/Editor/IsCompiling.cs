using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IsCompiling : EditorWindow
{
    [MenuItem("Window/IsCompiling Notifier")]
    private static void Init()
    {
        EditorWindow window = GetWindowWithRect(typeof(IsCompiling), new Rect(0, 0, 200, 30));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Compiling: ", EditorApplication.isCompiling ? "Yes" : "No");

        this.Repaint();
    }

}
