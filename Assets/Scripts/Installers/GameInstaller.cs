using UnityEngine;
using Reflex.Core;
using SmallBallBigPlane.Collectables;

namespace SmallBallBigPlane.Installers
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private WindowBase[] windows;
        [SerializeField] private GameObject background;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(GameManager));
            containerBuilder.AddSingleton(typeof(CoinManager));
            
            foreach (var window in windows)
            {
                containerBuilder.AddSingleton(window, window.GetType());
            }
            
            containerBuilder.AddSingleton(new WindowManager(windows, background));
        }
    }
}