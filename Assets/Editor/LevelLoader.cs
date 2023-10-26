using UnityEditor;
using UnityEngine;

namespace Editor {
    public class LevelLoader : EditorWindow {
        private Level selectedLevel;
        
        [MenuItem("Window/Level Loader")]
        public static void OpenWindow() {
            LevelLoader window = GetWindow<LevelLoader>();
            window.titleContent = new GUIContent("Level Loader");
        }

        private void OnGUI() {
            selectedLevel = (Level)EditorGUILayout.ObjectField(selectedLevel, typeof(Level));
            if (GUILayout.Button("Load") && selectedLevel != null)
                Debug.Log("Load!");
        }
    }
}