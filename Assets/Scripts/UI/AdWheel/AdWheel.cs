using Reflex.Attributes;
using SmallBallBigPlane.Collectables;
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

        [Inject] private readonly CoinManager _coinManager;
        
        private int _reward;
        private readonly int[] _modifiers = new[] { 2, 4, 6, 4, 2 };

        public int Mult { get; private set; }
        private int _prevMult;

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
            
            button.interactable = _reward > 0;
        }

        private void Arrow_OnOnValueChanged(float angle)
        {
            Mult = Mathf.Abs(angle) switch
            {
                <= 18 and >= 0 => _modifiers[2], // 12 hour
                <= 54 and > 18 => _modifiers[1], // 10 2 hour
                <= 90 and > 54 => _modifiers[0], // 9 3 hour
                _ => 1
            };

            if (_prevMult == Mult) return;

            _prevMult = Mult;

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
            
            button.interactable = false;
            
            // show rewarded ad here
            // on shawn callback add money
            
            _coinManager.AddRewardedCoins(_reward * Mult);
        }

        private void SetButtonText(int value)
        {
            buttonValue.SetText($"GET {value}");
        }
    }
}