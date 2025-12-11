using HelpersAndExtensions.SaveSystem;
using Reflex.Core;
using Reflex.Injectors;
using SmallBallBigPlane.Collectables;
using SmallBallBigPlane.Effects;
using SmallBallBigPlane.Infrastructure.FSM;
using SmallBallBigPlane.Infrastructure.FSM.States;
using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.Infrastructure.Services.AssetManagement;
using SmallBallBigPlane.Infrastructure.Services.Factories;
using SmallBallBigPlane.UI;
using SmallBallBigPlane.UI.Windows;
using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.DI.Installers
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private WindowsSO windows;
        [SerializeField] private GameSettingsSO gameSettings;
        [SerializeField] private LevelsSO levels;
        [SerializeField] private LoadingScreen loadingScreenPrefab;
        [SerializeField] private PickupEffectsHandler pickupEffectsHandler;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(JsonSerializer), typeof(ISerializer));
            containerBuilder.AddSingleton(typeof(SettingsSystem));
            containerBuilder.AddSingleton(typeof(GameStateService));
            containerBuilder.AddSingleton(typeof(CoinManager));
            containerBuilder.AddSingleton(windows);
            containerBuilder.AddSingleton(gameSettings);
            containerBuilder.AddSingleton(levels);

            BindServices(containerBuilder);
            BindFactories(containerBuilder);
            BindAssets(containerBuilder);
            BindWindows(containerBuilder);
            BindStates(containerBuilder);
        }

        private void BindWindows(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(WindowsService));

            containerBuilder.AddSingleton(c =>
            {
                LoadingScreen loadingScreen = c.Resolve<AssetProvider>()
                    .Instantiate<LoadingScreen>(loadingScreenPrefab.gameObject, inject: false);

                return loadingScreen;
            });
        }

        private void BindAssets(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(AssetProvider));

            containerBuilder.AddSingleton(c =>
            {
                PickupEffectsHandler effects = c.Resolve<AssetProvider>()
                    .Instantiate<PickupEffectsHandler>(pickupEffectsHandler.gameObject, inject: false);

                return effects;
            });
        }

        private void BindFactories(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(UIFactory));
        }

        private void BindStates(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(BootstrapState));
            containerBuilder.AddSingleton(typeof(MainMenuState));
            containerBuilder.AddSingleton(typeof(GameLoopState));
            containerBuilder.AddSingleton(typeof(ExitState));
            containerBuilder.AddSingleton(typeof(WinLevelState));
            containerBuilder.AddSingleton(typeof(LooseLevelState));
            containerBuilder.AddSingleton(typeof(LoadLevelState));
            containerBuilder.AddSingleton(typeof(RestartState));

            containerBuilder.AddSingleton(c => new StateMachine(
                c.Resolve<BootstrapState>(),
                c.Resolve<MainMenuState>(),
                c.Resolve<GameLoopState>(),
                c.Resolve<ExitState>(),
                c.Resolve<WinLevelState>(),
                c.Resolve<LooseLevelState>(),
                c.Resolve<LoadLevelState>(),
                c.Resolve<RestartState>()
            ));
        }

        private void BindServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(FileDataService));
            containerBuilder.AddSingleton(typeof(SaveLoadSystem));
            containerBuilder.AddSingleton(typeof(SceneLoader));
            containerBuilder.AddSingleton(typeof(LevelsManager));
            containerBuilder.AddSingleton(typeof(DebugService));
        }
    }
}