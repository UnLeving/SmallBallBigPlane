using UnityEngine;
using Reflex.Core;
using SmallBallBigPlane.Collectables;

namespace SmallBallBigPlane.Installers
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private PickupEffectsHandler pickupEffectsHandler;

        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(pickupEffectsHandler, typeof(IPickupEffectsHandler));
        }
    }
}