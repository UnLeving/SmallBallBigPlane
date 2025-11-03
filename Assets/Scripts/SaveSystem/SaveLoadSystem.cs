using System.Collections.Generic;
using System.Linq;

namespace HelpersAndExtensions.SaveSystem
{
    public interface ISaveLoadSystem 
    {
        GameData GameData { get; }
        void NewGame();
        void SaveGame();
        void LoadGame(string gameName);
        void ReloadGame();
        void DeleteGame(string gameName);
        void TryLoadGame();
    }

    public class SaveLoadSystem : ISaveLoadSystem
    {
        private const string DefaultSaveName = "New Game";
        
        public GameData GameData { get; private set; }
        private readonly IDataService _dataService;

        public SaveLoadSystem(IDataService dataService)
        {
            _dataService = dataService ?? throw new System.ArgumentNullException(nameof(dataService));
        }

        public void NewGame()
        {
            GameData = new GameData
            {
                Name = DefaultSaveName,
                //CurrentLevelName = "DemoScene"
            };
        }

        public void SaveGame()
        {
            if (GameData == null)
            {
                throw new System.InvalidOperationException("Cannot save: GameData is null. Call NewGame() or LoadGame() first.");
            }

            _dataService.Save(GameData);
        }

        public void TryLoadGame()
        {
            var saves = ListSaves();
            
            if (saves.Any())
            {
                LoadGame(saves.First());
                return;
            }
            
            NewGame();
        }

        public void LoadGame(string gameName = DefaultSaveName)
        {
            GameData = _dataService.Load(gameName);
        }

        public void ReloadGame()
        {
            if (GameData == null)
            {
                throw new System.InvalidOperationException("Cannot reload: GameData is null. Call NewGame() or LoadGame() first.");
            }

            LoadGame(GameData.Name);
        }

        public void DeleteGame(string gameName)
        {
            _dataService.Delete(gameName);
        }
        
        private IEnumerable<string> ListSaves()
        {
            return _dataService.ListSaves();
        }
    }
}