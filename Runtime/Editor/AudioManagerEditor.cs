using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CoverFrog.Audio
{
#if UNITY_EDITOR
    [CustomEditor(typeof(AudioManager)), CanEditMultipleObjects]
    public class AudioManagerEditor : Editor
    {
        private const string SaveDataName = "AudioPath.asset";
        private const string SaveEnumName = "AudioName.cs";

        private AudioPathData _audioPathData;
        private AudioManager _audioManager;

        public void OnEnable()
        {
            _audioManager = FindObjectOfType<AudioManager>();

            PathDataCheck();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(5);
            ClipUpdate();
            EditorGUILayout.Space(5);

            PathFind("Audio Clips Path", ref _audioPathData.pathAudioClipsPath);
        }

        private string PathAudio
        {
            get
            {
                var script  = MonoScript.FromScriptableObject(this);
                var folder  = AssetDatabase.GetAssetPath(script);
                var segment = folder.Split(new char[] { '/', '\\' });

                var parent  = string.Join("/", segment.Take(segment.Length - 2));
                var audioPath = Path.Combine(parent, "Audio");

                return audioPath;
            }
        }

        private void PathDataCheck()
        {
            var path_save   = Path.Combine(PathAudio, SaveDataName);

            var file_exist  = File.Exists(path_save);
            if (file_exist)
            {
                _audioPathData = AssetDatabase.LoadAssetAtPath(path_save, typeof(AudioPathData)) as AudioPathData;
            }

            else
            {
                var instance                = ScriptableObject.CreateInstance<AudioPathData>();
                instance.pathAudioClipsPath = "Assets";

                AssetDatabase.CreateAsset(instance, path_save);
                AssetDatabase.SaveAssets();

                _audioPathData = instance;
            }
        }

        private void PathFind(string subject, ref string path)
        {
            if (GUILayout.Button(subject, GUILayout.Width(110)))
            {
                var selectPath = EditorUtility.OpenFolderPanel("Select a file", Application.dataPath, "");

                PathSave(ref selectPath);

                if (!string.IsNullOrEmpty(selectPath))
                {
                    path = selectPath;

                    EditorUtility.SetDirty(_audioPathData);
                    AssetDatabase.SaveAssets();
                }
            }

            GUILayout.TextField(path, GUILayout.Height(20));
        }

        private void PathSave(ref string selectPath)
        {
            if (string.IsNullOrEmpty(selectPath)) return;

            string[] selectPaths  = selectPath.Split(new char[] { '/', '\\' });
            string rootFolderName = Util.RootFolderName;

            bool isRoot      = false;
            bool isAssets    = false;
            StringBuilder sb = new StringBuilder().Append("Assets");

            for (int i = 0; i < selectPaths.Length; i++)
            {
                string path = selectPaths[i];

                if (path == rootFolderName)
                {
                    isRoot = true;
                }
               

                if (isAssets)
                {
                    sb.Append('/').Append(path);
                }

                if (path == "Assets")
                {
                    isAssets = true;
                }
            }

            if (!isAssets || !isRoot)
            {
                Debug.LogError("Path is Must Place In Project");
                return;
            }

            selectPath = sb.ToString();
        }

        public void ClipUpdate()
        {
            if (!GUILayout.Button("Clip Update", GUILayout.Width(110))) return;

            if (!Directory.Exists(_audioPathData.pathAudioClipsPath))
            {
                Debug.LogError("Directory Not Exist");
                return;
            }


            string[] extensions = { "mp3", "wav" };
            string[] audioPaths = Directory.GetFiles(_audioPathData.pathAudioClipsPath);

            var audioClips = new List<AudioClip>();
            var enumData   = new StringBuilder();

            foreach (string audioPath in audioPaths)
            {
                string fileName = ClipPathParse(audioPath, extensions);

                if (string.IsNullOrEmpty(fileName)) continue;

                ClipEnumParse(fileName, ref enumData);
                ClipLoad(fileName, ref audioClips);
            }

            ClipEnumCreate(enumData.ToString());

            _audioManager.ClipUpdate(audioClips);
        }

        private string ClipPathParse(string path, string[] extensions)
        {
            string fileName        = Path.GetFileName(path);
            string[] fileNames     = fileName.Split('.');
            string fileExtension   = fileNames[fileNames.Length - 1];
            string speacialPattern = @"[^a-zA-Z0-9]";

            if (fileExtension != "meta" && Regex.IsMatch(fileNames[0], speacialPattern))
            {
                Debug.LogError($"[ { fileNames[0] } ] Is Contanin Special Char Or Space");
                return string.Empty;
            }

            if (fileNames.Length > 3)
            { 
                Debug.LogError($"[ { fileNames[0] } ] Is Contanin < . > Char ");
                return string.Empty;
            }

            foreach (string extension in extensions)
            {
                if (fileExtension == extension)
                {
                    return fileName;
                }
            }

            return string.Empty;
        }


        private void ClipEnumCreate(string datas)
        {
            string enumPath = Path.Combine(PathAudio, SaveEnumName);

            if (File.Exists(enumPath))
            {
                File.Delete(enumPath);
            }

            string enumName = SaveEnumName.Split('.')[0];
            string fileData = $@"namespace FrogLab.Audio
{{
    public enum {enumName}
    {{
{datas}
    }}
}}";
            File.WriteAllText(enumPath, fileData);

            AssetDatabase.Refresh();
        }

        private void ClipEnumParse(string fileName, ref StringBuilder enumData)
        {
            fileName = fileName.Split('.')[0];

            enumData.Append("\t").Append(fileName).Append(',').Append("\n");
        }

        private void ClipLoad(string fileName, ref List<AudioClip> audioClips)
        {
            string assetPath = Path.Combine(_audioPathData.pathAudioClipsPath, fileName);

            AudioClip clip   = AssetDatabase.LoadAssetAtPath<AudioClip>(assetPath);

            if (clip == null) return;

            audioClips.Add(clip);
        }
    }
#endif
}
