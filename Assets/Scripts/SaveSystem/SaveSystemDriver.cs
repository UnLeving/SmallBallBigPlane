using System.Collections.Generic;
using System.Linq;
using Reflex.Attributes;
using Reflex.Core;
using SmallBallBigPlane.Collectables;
using UnityEngine;
using UnityEngine.SceneManagement;
using HelpersAndExtensions.SaveSystem;

namespace SmallBallBigPlane
{
    public class SaveSystemDriver : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void EnsureExists()
        {
            if (FindObjectOfType<SaveSystemDriver>(true) != null) return;

            var go = new GameObject("SaveSystemDriver");
            DontDestroyOnLoad(go);
            go.AddComponent<SaveSystemDriver>();
        }

        [Inject]
        public void Construct(ISaveLoadSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
        }

        private ISaveLoadSystem _saveLoadSystem;

        private void Awake()
        {
            if (_saveLoadSystem == null)
            {
                var container = Container.ProjectContainer;
                _saveLoadSystem = container.Resolve<ISaveLoadSystem>();
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name is "MainMenuScene" or "LoadingScene") return;
            if (_saveLoadSystem.GameData == null) return;

            Bind<CoinManager, CoinData>(_saveLoadSystem.GameData.CoinData);
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
    }
}
