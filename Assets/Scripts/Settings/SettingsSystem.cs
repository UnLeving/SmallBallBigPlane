using UnityEngine;
using IngameDebugConsole;
using OmniSARTechnologies.LiteFPSCounter;
using HelpersAndExtensions.SaveSystem;

namespace SmallBallBigPlane
{
    [System.Serializable]
    public class SettingsData : ISavable
    {
        public string Id { get; set; }
        public bool LimitTo60Fps;
        public bool SoundEnabled;
        public bool ConsoleEnabled;
        public bool StatsEnabled;
    }
    
    public class SettingsSystem
    {
        private const string ConsolePrefabPath = "IngameDebugConsole";
        private const string FpsCounterPrefabPath = "LiteFPSCounter";

        public string Id { get; set; } = "Settings";
        public SettingsData data;
        private readonly SaveLoadSystem _saveLoadSystem;
        
        private GameObject _consoleInstance;
        private GameObject _fpsCounterInstance;
        
        public SettingsSystem(SaveLoadSystem saveLoadSystem)
        {
            this._saveLoadSystem = saveLoadSystem;
        }

        private void ApplyAll()
        {
            ApplyLimitFps();
            ApplySound();
            ApplyConsole();
            ApplyStats();
        }
        
        public void Initialize()
        {
            this.data = _saveLoadSystem.GameData.SettingsData;
            this.data.Id = Id;
            
            FromData(data);
        }
        
        public void SaveData()
        {
            if (data == null) return;

            var currentData = ToData();
            
            data.LimitTo60Fps = currentData.LimitTo60Fps;
            data.SoundEnabled = currentData.SoundEnabled;
            data.ConsoleEnabled = currentData.ConsoleEnabled;
            data.StatsEnabled = currentData.StatsEnabled;
        }

        public SettingsData ToData()
        {
            return new SettingsData
            {
                LimitTo60Fps = data.LimitTo60Fps,
                SoundEnabled = data.SoundEnabled,
                ConsoleEnabled = data.ConsoleEnabled,
                StatsEnabled = data.StatsEnabled
            };
        }

        public void FromData(SettingsData data)
        {
            if (data == null) return;
            
            this.data.LimitTo60Fps = data.LimitTo60Fps;
            this.data.SoundEnabled = data.SoundEnabled;
            this.data.ConsoleEnabled = data.ConsoleEnabled;
            this.data.StatsEnabled = data.StatsEnabled;
            
            ApplyAll();
        }

        public void ApplyLimitFps()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = this.data.LimitTo60Fps ? 60 : 30;
        }

        public void ApplySound()
        {
            AudioListener.volume = this.data.SoundEnabled ? 1f : 0f;
        }

        public void ApplyConsole()
        {
            EnsureConsoleInstance();
            if (_consoleInstance != null)
            {
                _consoleInstance.SetActive(this.data.ConsoleEnabled);
                var manager = DebugLogManager.Instance;
                if (manager != null)
                {
                    if (this.data.ConsoleEnabled) manager.ShowLogWindow(); else manager.HideLogWindow();
                }
            }
        }

        public void ApplyStats()
        {
            EnsureStatsInstance();
            if (_fpsCounterInstance != null)
            {
                _fpsCounterInstance.SetActive(this.data.StatsEnabled);
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

            var found = GameObject.Find("IngameDebugConsole");
            if (found != null)
            {
                _consoleInstance = found;
                Object.DontDestroyOnLoad(_consoleInstance);
                return;
            }

            var prefab = Resources.Load<GameObject>(ConsolePrefabPath);
            if (prefab != null)
            {
                _consoleInstance = Object.Instantiate(prefab);
                _consoleInstance.name = "IngameDebugConsole";
                Object.DontDestroyOnLoad(_consoleInstance);
                _consoleInstance.SetActive(false);
            }
        }
    }
}


