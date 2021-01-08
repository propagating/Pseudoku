using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class OnUnityLoad
{
    static string scenePath
    {
        get { return EditorPrefs.GetString("LoaderScene"); }
        set { EditorPrefs.SetString("LoaderScene", value); }
    }

    static OnUnityLoad()
    {
        EditorApplication.playmodeStateChanged = () =>
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                ////Adjust this value to whatever your index is for your preloader scene.
                //if (EditorSceneManager.GetActiveScene().buildIndex != 1)
                //{
                //    //Change the path here relative to your preloader scene.
                //    scenePath = EditorSceneManager.OpenScene("Assets/Scenes/SCGLogo.unity", OpenSceneMode.Additive).path;
                //}
                //If the current scene has been modified and is still not saved
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    //Console debug
                    Debug.Log("Auto-Saved opened scenes before entering Play mode");
                    //Sound
                    //EditorApplication.Beep();
                    //Save assets
                    AssetDatabase.SaveAssets();
                    //Save scenes, but ask the user before
                    //EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

                    //IF YOU DON'T WANT TO BE ASKED FOR CONFIRMATION, USE THIS INSTEAD :
                    EditorSceneManager.SaveOpenScenes();
                }
            }

            //if (EditorApplication.isPlayingOrWillChangePlaymode == false && !EditorApplication.isPlaying)
            //{
            //    EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByPath(scenePath), true);
            //}
        };
    }
}