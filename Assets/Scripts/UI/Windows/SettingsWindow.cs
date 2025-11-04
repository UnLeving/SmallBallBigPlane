using Cysharp.Threading.Tasks;
using HelpersAndExtensions.SaveSystem;
using UnityEngine;
using UnityEngine.UI;
using Reflex.Attributes;

namespace SmallBallBigPlane
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Toggle fps60Toggle;
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Toggle consoleToggle;
        [SerializeField] private Toggle statsToggle;
        [SerializeField] private Button closeButton;

        private ISettingsSystem _settingsSystem;
        private ISaveLoadSystem _saveLoadSystem;
        private bool _isUpdatingUI;
        private SettingsBinder _settingsBinder;

        [Inject]
        public void Construct(ISettingsSystem settingsSystem, ISaveLoadSystem saveLoadSystem)
        {
            _settingsSystem = settingsSystem;
            _saveLoadSystem = saveLoadSystem;
        }

        private void Awake()
        {
            fps60Toggle.onValueChanged.AddListener(On60FpsToggleValueChanged);
            soundToggle.onValueChanged.AddListener(OnSoundToggleValueChanged);
            consoleToggle.onValueChanged.AddListener(OnConsoleToggleValueChanged);
            statsToggle.onValueChanged.AddListener(OnStatsToggleValueChanged);
            
            closeButton.onClick.AddListener(OnCloseClicked);
            
            _settingsBinder = GetComponent<SettingsBinder>();

            SyncUIFromSettings();
        }
        
        private void OnEnable()
        {
            Show().Forget();
        }
        
        private void OnDestroy()
        {
            fps60Toggle.onValueChanged.RemoveListener(On60FpsToggleValueChanged);
            soundToggle.onValueChanged.RemoveListener(OnSoundToggleValueChanged);
            consoleToggle.onValueChanged.RemoveListener(OnConsoleToggleValueChanged);
            statsToggle.onValueChanged.RemoveListener(OnStatsToggleValueChanged);
            
            closeButton.onClick.RemoveListener(OnCloseClicked);
        }
        
        private void OnCloseClicked()
        {
            //gameObject.SetActive(false);
            
            Hide().Forget();
            
            _settingsBinder.SaveData();
            
            _saveLoadSystem.SaveGame();
        }
        
        private void On60FpsToggleValueChanged(bool value)
        {
            if (_isUpdatingUI) return;
            _settingsSystem.LimitTo60Fps = value;
            _settingsSystem.ApplyLimitFps();
        }

        private void OnSoundToggleValueChanged(bool value)
        {
            if (_isUpdatingUI) return;
            _settingsSystem.SoundEnabled = value;
            _settingsSystem.ApplySound();
        }

        private void OnConsoleToggleValueChanged(bool value)
        {
            if (_isUpdatingUI) return;
            _settingsSystem.ConsoleEnabled = value;
            _settingsSystem.ApplyConsole();
        }

        private void OnStatsToggleValueChanged(bool value)
        {
            if (_isUpdatingUI) return;
            _settingsSystem.StatsEnabled = value;
            _settingsSystem.ApplyStats();
        }

        private void SyncUIFromSettings()
        {
            _isUpdatingUI = true;
            fps60Toggle.isOn = _settingsSystem.LimitTo60Fps;
            soundToggle.isOn = _settingsSystem.SoundEnabled;
            consoleToggle.isOn = _settingsSystem.ConsoleEnabled;
            statsToggle.isOn = _settingsSystem.StatsEnabled;
            _isUpdatingUI = false;
        }
    }
}