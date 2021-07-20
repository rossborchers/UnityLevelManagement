
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement.Editor
{
    [InitializeOnLoad]
    public class LevelBootstrapper
    {
        public const string LAST_LOADED_EDITOR_SCENE = "LevelManagement/LastLoadedEditorScene";
        
        static LevelBootstrapper()
        {
            // Delaying until first editor tick so that the menu
            // will be populated before setting check state, and
            // re-apply correct action.
            // The same goes for getting the preference from EditorPrefs
            // as it might not be initialized during the constructor call time
            EditorApplication.delayCall += () =>
            {
                EditorSceneManager.activeSceneChangedInEditMode += (current, next) =>
                {
                    EditorPrefs.SetString(LAST_LOADED_EDITOR_SCENE, next.name);
                };

                EditorApplication.playModeStateChanged += change =>
                {
                    if (change == PlayModeStateChange.ExitingEditMode)
                    {
                        EnableBootstrapScene(true);
                    }
                };
                EnableBootstrapScene(true);
            };
        }

        private static void EnableBootstrapScene( bool logInfo = false)
        {
            string[] paths = AssetDatabase.FindAssets("t:LevelDefinitions");
            string foundGuid = null;
               
            if (paths.Length > 1)
            {
                string path = AssetDatabase.GUIDToAssetPath(paths[0]);
                Debug.LogError("[LevelBootstrapper] Found multiple level definitions assets in project." +
                               $" Please ensure there is only one. Using first found: {path}");
            }

            if (paths.Length > 0)
            {
                foundGuid = paths[0];
            }
            else
            {
                Debug.LogError("[LevelBootstrapper] Could not find level definitions asset. Please add one to your project.");
            }

            if (string.IsNullOrEmpty(foundGuid))
            {
                EditorSceneManager.playModeStartScene = null;
            }
            else
            {
                string foundPath = AssetDatabase.GUIDToAssetPath(foundGuid);
                LevelDefinitions definitions = (LevelDefinitions) AssetDatabase.LoadAssetAtPath(foundPath, typeof(LevelDefinitions));

                if (definitions.BootstrapStartSceneInEditor)
                {
                    EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(definitions.StartScene);

                    if (logInfo)
                    {
                        Debug.Log($"<b>[LevelManagement]</b> Loading <i>{definitions.StartScene.ScenePath}</i> on play.");
                    }
                }
                else
                {
                    EditorSceneManager.playModeStartScene = null;
                    if (logInfo)
                    {
                        Debug.Log($"<b>[LevelManagement]</b> Loading open scene on play.");
                    }
                }
             
            }
        }

    }
}