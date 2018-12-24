using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuItem_Game
{
    [MenuItem("__Game__/主场景", false, 1000)]
    public static void OpenMainScene()
    {
        if (!EditorApplication.isPlaying && EditorBuildSettings.scenes.Length > 0)
        {
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path);
            EditorGUIUtility.PingObject(EditorGUIUtility.Load(EditorBuildSettings.scenes[0].path));
        }
    }
}
