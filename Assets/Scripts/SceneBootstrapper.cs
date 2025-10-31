using Reflex.Attributes;
using SmallBallBigPlane.Collectables;
using UnityEngine;

namespace SmallBallBigPlane
{
    public class SceneBootstrapper : MonoBehaviour
    {
        [Inject] private ICoinManager _coins;

        private void Start()
        {
            var allCoins = FindObjectsOfType<Coin>(true);
            
            _coins.Initialize(allCoins);
        }
    }
}