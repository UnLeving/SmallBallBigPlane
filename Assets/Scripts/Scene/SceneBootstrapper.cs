using Reflex.Attributes;
using SmallBallBigPlane.Collectables;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class SceneBootstrapper : MonoBehaviour
    {
        private ICoinManager _coinManager;

        [Inject]
        private void Construct(ICoinManager coinManager)
        {
            this._coinManager = coinManager;
        }

        private void Start()
        {
            var allCoins = FindObjectsOfType<Coin>(true);
            
            _coinManager.Initialize(allCoins);
        }
    }
}