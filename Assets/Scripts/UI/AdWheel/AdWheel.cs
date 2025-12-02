using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SmallBallBigPlane.UI.AdWheel
{
    public sealed class AdWheel : MonoBehaviour
    {
        [SerializeField] private AdWheelArrow arrow;
        [SerializeField] private TextMeshProUGUI[] values;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI buttonValue;

        private int _reward;
        
        private readonly int[] _modifiers = new[] { 2, 4, 6, 4, 2 };

        public int Mult { get; private set; }
        
        private void Awake()
        {
            if (values == null || values.Length == 0)
            {
                Debug.LogError("AdWheel values array is null or empty");

                Debug.Break();
            }

            SetModifiers();
        }
        
        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
            
            arrow.OnValueChanged += Arrow_OnOnValueChanged;
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
            
            arrow.OnValueChanged -= Arrow_OnOnValueChanged;
        }

        public void Initialize(int reward)
        {
            _reward = reward;
            
            SetButtonText(reward);
            
            arrow.Enable();
        }

        private void Arrow_OnOnValueChanged(float angle)
        {
            Mult = Mathf.Abs(angle) switch
            {
                <= 18 and >= 0 => _modifiers[2],
                <= 54 and > 18 => _modifiers[1],
                <= 90 and > 54 => _modifiers[0],
                _ => 1
            };
            
            var total = _reward * Mult;
            
            SetButtonText(total);
        }
        
        private void SetModifiers()
        {
            for (var i = 0; i < values.Length; i++)
            {
                values[i].SetText($"x{_modifiers[i]}");
            }
        }

        private void OnClick()
        {
            arrow.Disable();
        }

        private void SetButtonText(int value)
        {
            buttonValue.SetText($"GET {value}");
        }
    }
}