using HelpersAndExtensions.SaveSystem.Example;

namespace HelpersAndExtensions.SaveSystem
{
    [System.Serializable]
    public class GameData
    {
        public string Name;
        public string CurrentLevelName;
        public PlayerData PlayerData;
    }
}