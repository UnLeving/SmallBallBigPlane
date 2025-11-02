using System.Collections.Generic;
using System.Linq;
using Reflex.Attributes;
using SmallBallBigPlane;
using UnityEngine;

namespace HelpersAndExtensions.SaveSystem
{
    public interface ISaveLoadSystem
    {
        void NewGame();
        void SaveGame();
        void LoadGame(string gameName);
        void ReloadGame();
        void DeleteGame(string gameName);
    }

    public class SaveLoadSystem : MonoBehaviour, ISaveLoadSystem
    {
        public GameData gameData;
        private Player _player;
        private IDataService _dataService;
        
        [Inject]
        private void Construct(Player player)
        {
            _player = player;
            
            _dataService = new FileDataService(new JsonSerializer());
            
            if (gameData == null)
            {
                Debug.Log("Game data is null. Creating new game");
                
                NewGame();
            }

            BindPlayer<Player, PlayerData>(gameData.PlayerData);
        }
        
        private void BindPlayer<T, TData>(TData data)
            where T : MonoBehaviour, IBind<TData>
            where TData : ISavable, new()
        {
            var entity = (IBind<TData>)_player;

            if (entity != null)
            {
                if (data == null)
                {
                    data = new TData { Id = entity.Id };
                }

                entity.Bind(data);
            }
        }

        private void Bind<T, TData>(TData data)
            where T : MonoBehaviour, IBind<TData>
            where TData : ISavable, new()
        {
            var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();

            if (entity != null)
            {
                if (data == null)
                {
                    data = new TData { Id = entity.Id };
                }

                entity.Bind(data);
            }
        }

        private void Bind<T, TData>(List<TData> datas)
            where T : MonoBehaviour, IBind<TData>
            where TData : ISavable, new()
        {
            var entities = FindObjectsByType<T>(FindObjectsSortMode.None);

            foreach (var entity in entities)
            {
                var data = datas.FirstOrDefault(d => d.Id == entity.Id);

                if (data == null)
                {
                    data = new TData { Id = entity.Id };

                    datas.Add(data);
                }

                entity.Bind(data);
            }
        }

        public void NewGame()
        {
            gameData = new GameData
            {
                Name = "New Game",
                CurrentLevelName = "DemoScene"
            };
        }

        public void SaveGame()
        {
            _dataService.Save(gameData);
        }

        public void LoadGame(string gameName)
        {
            gameData = _dataService.Load(gameName);

            // todo: load level through level manager

            // if (String.IsNullOrWhiteSpace(gameData.CurrentLevelName))
            // {
            //     gameData.CurrentLevelName = "DemoScene";
            // }

            //SceneManager.LoadScene(gameData.CurrentLevelName);
        }

        public void ReloadGame()
        {
            LoadGame(gameData.Name);
        }

        public void DeleteGame(string gameName)
        {
            _dataService.Delete(gameName);
        }
    }
}