namespace LevelManagement
{
    public class DefaultLevelManager : LevelManager
    {
        public void Awake()
        {
            LoadLevel(_levelDefinitions.Levels[0].LevelName);
        }
    }
}
