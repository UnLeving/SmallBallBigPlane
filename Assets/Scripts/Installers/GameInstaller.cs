using UnityEngine;
using Reflex.Core;
using SmallBallBigPlane.Collectables;

namespace SmallBallBigPlane.Installers
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private PickupEffectsHandler pickupEffectsHandler;
        [SerializeField] private CoinManager coinManager;
        [SerializeField] private GameSettingsSO gameSettings;
        [SerializeField] private LevelsSO levels;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(GameManager), typeof(IGameManager));
            containerBuilder.AddSingleton(coinManager, typeof(ICoinManager));
            containerBuilder.AddSingleton(typeof(WindowManager), typeof(IWindowManager));
            containerBuilder.AddSingleton(pickupEffectsHandler, typeof(IPickupEffectsHandler));
            
            containerBuilder.AddSingleton(gameSettings);
            
            containerBuilder.AddSingleton(levels);
        }
    }
}