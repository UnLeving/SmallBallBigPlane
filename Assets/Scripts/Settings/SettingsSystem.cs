using UnityEngine;
using IngameDebugConsole;
using OmniSARTechnologies.LiteFPSCounter;
using HelpersAndExtensions.SaveSystem;

namespace SmallBallBigPlane
{
    public class SettingsSystem : ISettingsSystem
    {
        private const string ConsolePrefabPath = "IngameDebugConsole";
        private const string FpsCounterPrefabPath = "LiteFPSCounter";

        private GameObject _consoleInstance;
        private GameObject _fpsCounterInstance;

        public bool LimitTo60Fps { get; set; } = true;
        public bool SoundEnabled { get; set; } = true;
        public bool ConsoleEnabled { get; set; } = false;
        public bool StatsEnabled { get; set; } = false;

        public SettingsSystem()
        {
            ApplyAll();
        }

        public void ApplyAll()
        {
            ApplyLimitFps();
            ApplySound();
            ApplyConsole();
            ApplyStats();
        }

        public SettingsData ToData()
        {
            return new SettingsData
            {
                LimitTo60Fps = LimitTo60Fps,
                SoundEnabled = SoundEnabled,
                ConsoleEnabled = ConsoleEnabled,
                StatsEnabled = StatsEnabled
            };
        }

        public void FromData(SettingsData data)
        {
            if (data == null) return;
            
            LimitTo60Fps = data.LimitTo60Fps;
            SoundEnabled = data.SoundEnabled;
            ConsoleEnabled = data.ConsoleEnabled;
            StatsEnabled = data.StatsEnabled;
            ApplyAll();
        }

        public void ApplyLimitFps()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = LimitTo60Fps ? 60 : 30;
        }

        public void ApplySound()
        {
            AudioListener.volume = SoundEnabled ? 1f : 0f;
        }

        public void ApplyConsole()
        {
            EnsureConsoleInstance();
            if (_consoleInstance != null)
            {
                _consoleInstance.SetActive(ConsoleEnabled);
                var manager = DebugLogManager.Instance;
                if (manager != null)
                {
                    if (ConsoleEnabled) manager.ShowLogWindow(); else manager.HideLogWindow();
                }
            }
        }

        public void ApplyStats()
        {
            EnsureStatsInstance();
            if (_fpsCounterInstance != null)
            {
                _fpsCounterInstance.SetActive(StatsEnabled);
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


