using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.Infrastructure.Services.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace SmallBallBigPlane.UI
{
    public sealed class SettingsButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        [Inject] private readonly WindowsService _windowsService;
        
        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _windowsService.Show(WindowId.Settings).Forget();
        }
    }
}