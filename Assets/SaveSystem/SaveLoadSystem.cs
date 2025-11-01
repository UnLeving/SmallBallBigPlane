using System;
using System.Collections.Generic;
using System.Linq;
using HelpersAndExtensions.SaveSystem.Example;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HelpersAndExtensions.SaveSystem
{
    public class SaveLoadSystem : MonoBehaviour
    {
        public static SaveLoadSystem Instance { get; private set; }


        public GameData gameData;

        private IDataService _dataService;

        private void Awake()
        {
            Instance = this;

            _dataService = new FileDataService(new JsonSerializer());

            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneManager_OnsceneLoaded;
        }
        
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneManager_OnsceneLoaded;
        }

        private void SceneManager_OnsceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "MainMenuScene") return;
            
            Bind<Hero, PlayerData>(gameData.PlayerData);
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

            SceneManager.LoadScene(gameData.CurrentLevelName);
        }

        public void SaveGame()
        {
            _dataService.Save(gameData);
        }

        public void LoadGame(string gameName)
        {
            gameData = _dataService.Load(gameName);

            if (String.IsNullOrWhiteSpace(gameData.CurrentLevelName))
            {
                gameData.CurrentLevelName = "DemoScene";
            }

            SceneManager.LoadScene(gameData.CurrentLevelName);
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