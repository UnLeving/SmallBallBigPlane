using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Reflex.Core;
using SmallBallBigPlane;
using SmallBallBigPlane.Collectables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HelpersAndExtensions.SaveSystem
{
    public class SaveLoadSystem : IService
    {
        private const string DefaultSaveName = "New Game";

        public GameData GameData { get; private set; }
        private readonly IDataService _dataService;

        public SaveLoadSystem(FileDataService dataService)
        {
            _dataService = dataService ?? throw new System.ArgumentNullException(nameof(dataService));
        }
        
        private void NewGame()
        {
            if (GameData != null)
            {
                return;
            }

            GameData = new GameData
            {
                Name = DefaultSaveName,
            };

            SaveGame();
        }

        public void SaveGame()
        {
            if (GameData == null)
            {
                NewGame();

                Debug.Log("Cannot save: GameData is null. Calling NewGame().");
            }

            _dataService.Save(GameData);
        }

        public async UniTask TryLoadGame()
        {
            var saves = ListSaves();

            if (saves.Any())
            {
                await LoadGame(saves.First());

                return;
            }

            NewGame();
        }

        private async UniTask LoadGame(string gameName = DefaultSaveName)
        {
            GameData = await _dataService.Load(gameName);
        }
        
        private IEnumerable<string> ListSaves()
        {
            return _dataService.ListSaves();
        }
    }
}