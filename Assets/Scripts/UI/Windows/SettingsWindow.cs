using Cysharp.Threading.Tasks;
using HelpersAndExtensions.SaveSystem;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace SmallBallBigPlane.UI.Windows
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Toggle fps60Toggle;
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Toggle consoleToggle;
        [SerializeField] private Toggle statsToggle;
        [SerializeField] private Button closeButton;

        private SettingsSystem _settingsSystem;
        private SaveLoadSystem _saveLoadSystem;
        private bool _isUpdatingUI;

        [Inject]
        public void Construct(SettingsSystem settingsSystem, SaveLoadSystem saveLoadSystem)
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
            Hide().Forget();
            
            _settingsSystem.SaveData();
            
            _saveLoadSystem.SaveGame();
        }
        
        private void On60FpsToggleValueChanged(bool value)
        {
            if (_isUpdatingUI) return;
            _settingsSystem.data.LimitTo60Fps = value;
            _settingsSystem.ApplyLimitFps();
        }

        private void OnSoundToggleValueChanged(bool value)
        {
            if (_isUpdatingUI) return;
            _settingsSystem.data.SoundEnabled = value;
            _settingsSystem.ApplySound();
        }

        private void OnConsoleToggleValueChanged(bool value)
        {
            if (_isUpdatingUI) return;
            _settingsSystem.data.ConsoleEnabled = value;
            _settingsSystem.ApplyConsole();
        }

        private void OnStatsToggleValueChanged(bool value)
        {
            if (_isUpdatingUI) return;
            _settingsSystem.data.StatsEnabled = value;
            _settingsSystem.ApplyStats();
        }

        private void SyncUIFromSettings()
        {
            _isUpdatingUI = true;
            fps60Toggle.isOn = _settingsSystem.data.LimitTo60Fps;
            soundToggle.isOn = _settingsSystem.data.SoundEnabled;
            consoleToggle.isOn = _settingsSystem.data.ConsoleEnabled;
            statsToggle.isOn = _settingsSystem.data.StatsEnabled;
            _isUpdatingUI = false;
        }

        public override async UniTask Show()
        {
            if (isOpened) return;
        
            isOpened = true;
        
            await ShowPanel();
        }
        
        public override async UniTask Hide()
        {
            if (!isOpened) return;
        
            isOpened = false;
        
            await HidePanel();
        }
    }
}