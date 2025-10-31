using Reflex.Core;
using UnityEngine;

namespace SmallBallBigPlane.Installers
{
    public class ProjectInstaller: MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(SceneLoader), typeof(ISceneLoader));
        }
    }
}