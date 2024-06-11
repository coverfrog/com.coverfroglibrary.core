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

        private static List<string> BuildSceneNames
        {
            get
            {
                EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

                List<string> scene_names = new List<string>(scenes.Length);

                foreach (EditorBuildSettingsScene scene in scenes)
                {
                    string scene_path = scene.path;

                    if (string.IsNullOrEmpty(scene_path) || !File.Exists(scene_path)) continue;

                    scene_names.Add(scene_path);
                }

                return scene_names;
            }
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

            foreach (string path in BuildSceneNames)
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

