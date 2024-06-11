#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

namespace CoverFrog
{
    public class SceneMoveEditor : EditorWindow
    {
        private Vector2 scrollPosition = Vector2.zero; 

        [MenuItem("__CoverFrog__/Scene Window")]
        public static void ShowWindow()
        {
            var window = EditorWindow.GetWindow(typeof(SceneMoveEditor));
            window.titleContent = new GUIContent("Scene Window");
        }

        private void OnGUI()
        {
            GUILayout.Label("Move Scenes", new GUIStyle() { 
                fontSize = 15,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                padding = new RectOffset(5, 10, 0, 0),
                fixedHeight = 30,
                normal = new GUIStyleState() { 
                    textColor = Color.white,
                }
            });

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            foreach (string path in Util.BuildSceneNames)
            {
                string sceneName = Path.GetFileNameWithoutExtension(path);

                if (GUILayout.Button(sceneName, GUILayout.Height(30)))
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene(path);
                }
            }

            GUILayout.EndScrollView();
        }
    }
}
#endif

