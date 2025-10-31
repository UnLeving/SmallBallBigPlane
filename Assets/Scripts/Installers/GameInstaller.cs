using UnityEngine;
using Reflex.Core;
using SmallBallBigPlane.Collectables;

namespace SmallBallBigPlane.Installers
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(GameManager), typeof(IGameManager));
            containerBuilder.AddSingleton(typeof(CoinManager), typeof(ICoinManager));
            containerBuilder.AddSingleton(typeof(WindowManager), typeof(IWindowManager));
            containerBuilder.AddSingleton(typeof(SceneLoader), typeof(ISceneLoader));
        }
    }
}