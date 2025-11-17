using SmallBallBigPlane;
using SmallBallBigPlane.Collectables;

namespace HelpersAndExtensions.SaveSystem
{
    [System.Serializable]
    public class GameData
    {
        public string Name;
        public CoinData CoinData;
        public SettingsData SettingsData;
        
        public override string ToString()
        {
            return $"GameData: {Name}, Coins: {CoinData?.Id}, Settings: {SettingsData?.Id}";
        }
    }
}