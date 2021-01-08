using UnityEditor;
using UnityEngine;

public class SavePathMenus
{
    [MenuItem("View/Open Save Path")]
    public static void OpenSavePath()
    {
        var itemPath = Application.persistentDataPath.Replace(@"/", @"\") + @"\";
        System.Diagnostics.Process.Start("explorer.exe", itemPath);
    }
}