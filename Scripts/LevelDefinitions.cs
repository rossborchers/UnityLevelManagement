using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    [CreateAssetMenu(menuName = "LevelManagement/LevelDefinitions")]
    public class LevelDefinitions : ScriptableObject
    {
        public bool BootstrapStartSceneInEditor;
        public SceneReference StartScene;
        public List<Level> Levels = new List<Level>();

        public List<string> LevelNames
        {
            get
            {
                List<string> levelNames = new List<string>();
                foreach (Level l in Levels)
                {
                    levelNames.Add(l.LevelName);
                }

                return levelNames;
            }
        }

        [Serializable]
        public class Level
        {
            public string LevelName;
            public List<SceneReference> SceneStack;
        }
    }
}