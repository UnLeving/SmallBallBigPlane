using Reflex.Core;
using UnityEngine;

namespace SmallBallBigPlane.Installers
{
    public class ProjectInstaller: MonoBehaviour, IInstaller
    {
        [SerializeField] private PickupSFX pickupSfxPrefab;
        [SerializeField] private PickupVFX pickupVFXPrefab;
        
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(pickupSfxPrefab);
            containerBuilder.AddSingleton(pickupVFXPrefab);
        }
    }
}