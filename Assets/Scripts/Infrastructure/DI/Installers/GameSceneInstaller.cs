using Reflex.Core;
using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.DI.Installers
{
    public sealed class GameSceneInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private UICamera uiCamera;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(uiCamera);
        }
    }
}