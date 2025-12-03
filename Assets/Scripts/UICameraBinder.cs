using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    public sealed class UICameraBinder : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        
        [Inject] private readonly UICamera _uiCamera;

        private void Awake()
        {
            if (!canvas)
            {
                Debug.LogError($"UICamera {name} is missing");
            }
        }

        private void Start()
        {
            canvas.worldCamera = _uiCamera.Camera;
        }
    }
}