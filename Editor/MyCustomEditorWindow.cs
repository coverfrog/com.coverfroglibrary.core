using UnityEditor;
using UnityEngine;

namespace CoverFrog.Editor
{
    public class MyCustomEditorWindow : EditorWindow
    {
        private string myString = "Hello, World!";
        private int myInt = 42;

        [MenuItem("__CoverFrog__/My Custom Editor Window")]
        public static void ShowWindow()
        {
            GetWindow<MyCustomEditorWindow>("My Custom Editor Window");
        }

        private void OnGUI()
        {
            GUILayout.Label("This is a custom editor window", EditorStyles.boldLabel);

            myString = EditorGUILayout.TextField("Text Field", myString);
            myInt = EditorGUILayout.IntField("Int Field", myInt);

            if (GUILayout.Button("Press Me"))
            {
                Debug.Log("Button Pressed!");
            }
        }
    }
}

