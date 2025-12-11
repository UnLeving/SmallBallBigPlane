using IngameDebugConsole;
using OmniSARTechnologies.LiteFPSCounter;
using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.Services
{
    public class DebugService : IService
    {
        public string Id { get; set; } = "DebugService";
        
        private const string ConsolePrefabPath = "IngameDebugConsole";
        private const string FpsCounterPrefabPath = "LiteFPSCounter";

        private readonly SettingsSystem _settingsSystem;
        private GameObject _consoleInstance;
        private GameObject _fpsCounterInstance;

        public DebugService(SettingsSystem settingsSystem)
        {
            _settingsSystem = settingsSystem;
        }

        public void Initialize()
        {
            _settingsSystem.OnSettingsChanged += OnSettingsChanged;
            
            ApplyConsole(_settingsSystem.data.ConsoleEnabled);
            ApplyStats(_settingsSystem.data.StatsEnabled);
        }

        private void OnSettingsChanged(SettingsData data)
        {
            ApplyConsole(data.ConsoleEnabled);
            ApplyStats(data.StatsEnabled);
        }

        public void ApplyConsole(bool isEnabled)
        {
            EnsureConsoleInstance();

            if (_consoleInstance == null) return;
            
            _consoleInstance.SetActive(isEnabled);
                
            var manager = DebugLogManager.Instance;

            if (manager == null) return;
                
            if (isEnabled)
            {
                manager.ShowLogWindow();
            }
            else
            {
                manager.HideLogWindow();
            }
        }

        public void ApplyStats(bool isEnabled)
        {
            EnsureStatsInstance();
            if (_fpsCounterInstance != null)
            {
                _fpsCounterInstance.SetActive(isEnabled);
            }
        }

        private void EnsureStatsInstance()
        {
            if (_fpsCounterInstance != null) return;

            var existing = Object.FindObjectOfType<LiteFPSCounter>(true);
            if (existing != null)
            {
                _fpsCounterInstance = existing.gameObject;
                Object.DontDestroyOnLoad(_fpsCounterInstance);
                return;
            }

            var prefab = Resources.Load<GameObject>(FpsCounterPrefabPath);
            if (prefab != null)
            {
                _fpsCounterInstance = Object.Instantiate(prefab);
                _fpsCounterInstance.name = "LiteFPSCounter";
                Object.DontDestroyOnLoad(_fpsCounterInstance);
                _fpsCounterInstance.SetActive(false);
            }
        }

        private void EnsureConsoleInstance()
        {
            if (_consoleInstance != null) return;

            if (DebugLogManager.Instance != null)
            {
                _consoleInstance = DebugLogManager.Instance.transform.root.gameObject;
                Object.DontDestroyOnLoad(_consoleInstance);
                return;
            }

            var found = GameObject.FindWithTag("IngameDebugConsole");
            
            if (found != null)
            {
                _consoleInstance = found;
                Object.DontDestroyOnLoad(_consoleInstance);
                return;
            }

            var prefab = Resources.Load<GameObject>(ConsolePrefabPath);
            
            if (prefab == null) return;
            
            _consoleInstance = Object.Instantiate(prefab);
            _consoleInstance.name = "IngameDebugConsole";
            Object.DontDestroyOnLoad(_consoleInstance);
            _consoleInstance.SetActive(false);
        }
    }
}
