using Reflex.Core;
using UnityEngine;

namespace SmallBallBigPlane.Installers
{
    public class ProjectInstaller: MonoBehaviour, IInstaller
    {
        [SerializeField] private PickupSoundEffect pickupSoundEffectPrefab;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(pickupSoundEffectPrefab);
        }
    }
}