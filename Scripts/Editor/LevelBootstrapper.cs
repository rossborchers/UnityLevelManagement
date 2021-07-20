
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement.Editor
{
    [InitializeOnLoad]
    public class LevelBootstrapper
    {
        public const string BOOTSTRAP_TOGGLE = "LevelManagement/Bootstrap Base Level";
        public const string AUTO_BOOTSTRAP_CURRENT_SCENE_TOGGLE = "LevelManagement/Auto Bootstrap Open Scene";
        public const string AUTO_BOOTSTRAP_EDITOR_SCENE = "LevelManagement/LastLoadedEditorScene";

        static LevelBootstrapper()
        {
            /// Delaying until first editor tick so that the menu
            /// will be populated before setting check state, and
            /// re-apply correct action.
            /// The same goes for getting the preference from EditorPrefs
            /// as it might not be initialized during the constructor call time
            EditorApplication.delayCall += () =>
            {
                EditorSceneManager.activeSceneChangedInEditMode += (Scene current, Scene next) =>
                {
                    Debug.Log("Active Scene changed to: " + next.name);
                    EditorPrefs.SetString(AUTO_BOOTSTRAP_EDITOR_SCENE, next.name);
                };

                if (EditorBuildSettings.scenes.Length > 0)
                {
                    bool bootstrapOn = EditorPrefs.GetBool(BOOTSTRAP_TOGGLE, true);
                    EnableBootstrapScene(bootstrapOn);
                }
            };
        }

        private static void EnableBootstrapScene(bool enable)
        {
            if (enable)
            {
                // Set Play Mode scene to first scene defined in build settings. This is assumed to be the bootstrapper
                EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
            }
            else
            {
                EditorSceneManager.playModeStartScene = null;
            }
        }

        [MenuItem(BOOTSTRAP_TOGGLE)]
        private static void ToggleBootstrapper()
        {
            bool bootstrapOn = EditorPrefs.GetBool(BOOTSTRAP_TOGGLE, true);
            bootstrapOn = !bootstrapOn;

            EditorPrefs.SetBool(BOOTSTRAP_TOGGLE, bootstrapOn);
            Menu.SetChecked(BOOTSTRAP_TOGGLE, bootstrapOn);

            EnableBootstrapScene(bootstrapOn);
        }

        [MenuItem(BOOTSTRAP_TOGGLE, validate = true)]
        private static bool ValidateToggleBootstrapper()
        {
            bool bootstrapOn = EditorPrefs.GetBool(BOOTSTRAP_TOGGLE, true);
            Menu.SetChecked(BOOTSTRAP_TOGGLE, bootstrapOn);

            // Ensure at least one build scene exist.
            return EditorBuildSettings.scenes.Length > 0;
        }

        [MenuItem(AUTO_BOOTSTRAP_CURRENT_SCENE_TOGGLE)]
        private static void ToggleAutoBootstrapCurrentScene()
        {
            bool autoBootstrapCurrentScene = EditorPrefs.GetBool(AUTO_BOOTSTRAP_CURRENT_SCENE_TOGGLE, false);
            autoBootstrapCurrentScene = !autoBootstrapCurrentScene;

            EditorPrefs.SetBool(AUTO_BOOTSTRAP_CURRENT_SCENE_TOGGLE, autoBootstrapCurrentScene);
            Menu.SetChecked(AUTO_BOOTSTRAP_CURRENT_SCENE_TOGGLE, autoBootstrapCurrentScene);
        }

        [MenuItem(AUTO_BOOTSTRAP_CURRENT_SCENE_TOGGLE, validate = true)]
        private static bool ValidateToggleAutoBootstrapCurrentScene()
        {
            bool autoBootstrapCurrentScene = EditorPrefs.GetBool(AUTO_BOOTSTRAP_CURRENT_SCENE_TOGGLE, false);
            Menu.SetChecked(AUTO_BOOTSTRAP_CURRENT_SCENE_TOGGLE, autoBootstrapCurrentScene);
            return true;
        }
    }
}