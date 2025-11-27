using SmallBallBigPlane;
using SmallBallBigPlane.Collectables;
using SmallBallBigPlane.Infrastructure.Services;

namespace HelpersAndExtensions.SaveSystem
{
    [System.Serializable]
    public class GameData
    {
        public string Name;
        public CoinData CoinData;
        public SettingsData SettingsData;
        public LevelData LevelData;
        
        public override string ToString()
        {
            return $"GameData: {Name}, " +
                   $"Coins: {CoinData?.Id}, " +
                   $"Settings: {SettingsData?.Id}" +
                   $"Level: {LevelData?.Id}";
        }
    }
}