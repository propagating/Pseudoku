using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SwapInPlace : EditorWindow
{
    private bool selected;
    // the GameObject to be 'Swapped' in
    private GameObject previewGameObject;
    private Editor gameObjectEditor;

    [MenuItem("Window/Swap in Place", priority = 1)]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        SwapInPlace window = (SwapInPlace)EditorWindow.GetWindow(typeof(SwapInPlace));
        window.Show();
    }

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SwapInPlace));
    }

    private void OnGUI()
    {
        // create space at the top of the window as 'padding'
        EditorGUILayout.Space();

        #region Preview a new GameObject

        // Field for the gameObject to preview
        EditorGUIUtility.labelWidth = 90.0f;
        previewGameObject = (GameObject)EditorGUILayout.ObjectField("Preview", previewGameObject, typeof(GameObject), true);
        EditorGUIUtility.labelWidth = 0;

        // set the style of the preview
        GUIStyle bgColor = new GUIStyle();
        bgColor.normal.background = EditorGUIUtility.whiteTexture;

        // create preview
        if (previewGameObject != null)
        {
            if (gameObjectEditor == null)
            {
                gameObjectEditor = Editor.CreateEditor(previewGameObject);
            }

            if (GUI.changed)
            {
                gameObjectEditor = Editor.CreateEditor(previewGameObject);
            }

            gameObjectEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(256, 256), bgColor);
        }

        #endregion

        #region Button to 'Swap' GameObjects

        // Make selected the current GameObject selected in the scene or heirarchy
        selected = Selection.activeGameObject;

        // if object in scene or heirarchy is selected show a 'Swap' button
        if (selected)
        {
            // if the 'Swap' button is pressed swap place the new object then destroy the existing item
            if (GUILayout.Button("Swap"))
            {
                Instantiate(previewGameObject, Selection.activeTransform.position, Quaternion.identity);
                DestroyImmediate(Selection.activeGameObject);
            }
        }

        #endregion
    }
}