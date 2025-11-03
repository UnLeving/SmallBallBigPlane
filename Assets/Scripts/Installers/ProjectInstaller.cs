using HelpersAndExtensions.SaveSystem;
using Reflex.Core;
using UnityEngine;

namespace SmallBallBigPlane.Installers
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(SceneLoader), typeof(ISceneLoader));
            
            containerBuilder.AddSingleton(typeof(JsonSerializer), typeof(ISerializer));
            containerBuilder.AddSingleton(typeof(FileDataService), typeof(IDataService));
            containerBuilder.AddSingleton(typeof(SaveLoadSystem), typeof(ISaveLoadSystem));
        }
    }
}