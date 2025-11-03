using Reflex.Attributes;
using SmallBallBigPlane.Collectables;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class SceneBootstrapper : MonoBehaviour
    {
        [SerializeField] private WindowBase[] windows;
        
        [Inject] private ICoinManager _coins;
        [Inject] private IWindowManager _windowManager;

        private void Start()
        {
            var allCoins = FindObjectsOfType<Coin>(true);
            
            _coins.Initialize(allCoins);
            _windowManager.Initialize(windows);
        }
    }
}