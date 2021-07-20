using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="LevelManagement/LevelDefinition")]
public class LevelDefinition : ScriptableObject
{
    public List<Level> Levels = new List<Level>();

    public List<string> LevelNames
    {
        get
        {
            List<string> levelNames = new List<string>();
            foreach(Level l in Levels)
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
        public List<string> SceneStack;
    }
}
