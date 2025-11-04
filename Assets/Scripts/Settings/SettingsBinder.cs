using UnityEngine;
using HelpersAndExtensions.SaveSystem;
using Reflex.Attributes;

namespace SmallBallBigPlane
{
    [System.Serializable]
    public class SettingsData : ISavable
    {
        [field: SerializeField] public string Id { get; set; }
        public bool LimitTo60Fps;
        public bool SoundEnabled;
        public bool ConsoleEnabled;
        public bool StatsEnabled;
    }

    public class SettingsBinder : MonoBehaviour, IBind<SettingsData>
    {
        [field: SerializeField] public string Id { get; set; } = "Settings";
        [SerializeField] private SettingsData data;

        private ISettingsSystem _settingsSystem;

        [Inject]
        public void Construct(ISettingsSystem settingsSystem)
        {
            _settingsSystem = settingsSystem;
        }

        public void Bind(SettingsData data)
        {
            if (data == null)
            {
                Debug.Log("SettingsData is null.");
                
                return;
            }
        
            this.data = data;
            this.data.Id = Id;
            
            _settingsSystem.FromData(data);
        }

        public void SaveData()
        {
            if (data == null) return;

            var currentData = _settingsSystem.ToData();
            
            data.LimitTo60Fps = currentData.LimitTo60Fps;
            data.SoundEnabled = currentData.SoundEnabled;
            data.ConsoleEnabled = currentData.ConsoleEnabled;
            data.StatsEnabled = currentData.StatsEnabled;
        }
    }
}