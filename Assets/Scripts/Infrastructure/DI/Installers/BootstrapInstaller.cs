using HelpersAndExtensions;
using Reflex.Core;
using SmallBallBigPlane.Infrastructure.DI.Installers;
using UnityEngine;

namespace SmallBallBigPlane.Installers
{
    public class BootstrapInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.OnContainerBuilt += ContainerBuilder_OnOnContainerBuilt;
        }

        private void ContainerBuilder_OnOnContainerBuilt(Container container)
        {
            FindAnyObjectByType<GameBootstrapper>(FindObjectsInactive.Include).Activate();
        }
    }
}