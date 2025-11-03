using Reflex.Attributes;
using SmallBallBigPlane.Collectables;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class SceneBootstrapper : MonoBehaviour
    {
        [SerializeField] private WindowBase[] windows;
        
        private ICoinManager _coinManager;
        private IWindowManager _windowManager;

        [Inject]
        private void Construct(ICoinManager coinManager, IWindowManager windowManager)
        {
            this._coinManager = coinManager;
            this._windowManager = windowManager;
        }

        private void Start()
        {
            var allCoins = FindObjectsOfType<Coin>(true);
            
            _coinManager.Initialize(allCoins);
            _windowManager.Initialize(windows);
        }
    }
}