using Reflex.Core;
using UnityEngine;

namespace SmallBallBigPlane.Installers
{
    public class ProjectInstaller: MonoBehaviour, IInstaller
    {
        [SerializeField] private PickupSoundEffect pickupSoundEffectPrefab;
        [SerializeField] private PickupVFX pickupVFXPrefab;
        
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(pickupSoundEffectPrefab);
            containerBuilder.AddSingleton(pickupVFXPrefab);
        }
    }
}