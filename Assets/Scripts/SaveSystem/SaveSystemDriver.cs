using System.Collections.Generic;
using System.Linq;
using Reflex.Attributes;
using SmallBallBigPlane.Collectables;
using UnityEngine;
using UnityEngine.SceneManagement;
using HelpersAndExtensions.SaveSystem;

namespace SmallBallBigPlane
{
    public class SaveSystemDriver : MonoBehaviour
    {
        private ISaveLoadSystem _saveLoadSystem;
        
        [Inject]
        public void Construct(ISaveLoadSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
        }
        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (_saveLoadSystem.GameData == null)
            {
                _saveLoadSystem.TryLoadGame();
            }

            switch (scene.name)
            {
                case nameof(SceneLoader.Scene.MainMenuScene):
                    Bind<SettingsBinder, SettingsData>(_saveLoadSystem.GameData.SettingsData);
                    break;
                case nameof(SceneLoader.Scene.GameScene):
                    Bind<CoinManager, CoinData>(_saveLoadSystem.GameData.CoinData);
                    break;
            }
        }

        private void Bind<T, TData>(TData data)
            where T : MonoBehaviour, IBind<TData>
            where TData : ISavable, new()
        {
            var entity = FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None).FirstOrDefault();

            if (entity != null)
            {
                if (data == null)
                {
                    data = new TData { Id = entity.Id };
                }

                entity.Bind(data);
            }
            else
            {
                Debug.LogError($"SaveSystemDriver.Bind: Cannot find {typeof(T).Name} in scene.");
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
    }
}