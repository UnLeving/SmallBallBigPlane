using HelpersAndExtensions.SaveSystem;
using UnityEngine;
using Reflex.Core;
using SmallBallBigPlane.Collectables;

namespace SmallBallBigPlane.Installers
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private PickupEffectsHandler pickupEffectsHandler;
        [SerializeField] private SaveLoadSystem saveLoadSystem;
        [SerializeField] private Player player;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(player);
            containerBuilder.AddSingleton(saveLoadSystem, typeof(ISaveLoadSystem));
            containerBuilder.AddSingleton(typeof(GameManager), typeof(IGameManager));
            containerBuilder.AddSingleton(typeof(CoinManager), typeof(ICoinManager));
            containerBuilder.AddSingleton(typeof(WindowManager), typeof(IWindowManager));
            containerBuilder.AddSingleton(pickupEffectsHandler, typeof(IPickupEffectsHandler));
        }
    }
}