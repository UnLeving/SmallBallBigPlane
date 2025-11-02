using System.Collections.Generic;
using System.Linq;
using SmallBallBigPlane.Collectables;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

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
        private const string DefaultSaveName = "New Game";
        
        public static SaveLoadSystem Instance { get; private set; }
        
        
        public GameData gameData;
        private IDataService _dataService;

        private void Awake()
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
            
            _dataService = new FileDataService(new JsonSerializer());
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneManager_OnsceneLoaded;
        }

        private void SceneManager_OnsceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        { 
            if(scene.name is "MainMenuScene" or "LoadingScene") return;
            
            //Bind<Player, PlayerData>(gameData.PlayerData);
            Bind<CoinManager, CoinData>(gameData.CoinData);
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneManager_OnsceneLoaded;
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
                Name = DefaultSaveName,
                //CurrentLevelName = "DemoScene"
            };
        }

        public void SaveGame()
        {
            _dataService.Save(gameData);
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
            gameData = _dataService.Load(gameName);
        }

        public void ReloadGame()
        {
            LoadGame(gameData.Name);
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