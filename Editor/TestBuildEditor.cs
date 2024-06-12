using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CoverFrog.Core.Editor
{
    [CustomEditor(typeof(TestBuild))]
    public class TestBuildEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            TestBuild test = (TestBuild)target;

            test.myString = EditorGUILayout.TextField("My String", test.myString);
            test.myInt = EditorGUILayout.IntField("My Int", test.myInt);

            if (GUILayout.Button("My Custom Button"))
            {
                Debug.Log("Button Pressed!");
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(test);
            }
        }
    }
}
