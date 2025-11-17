using HelpersAndExtensions.SaveSystem;
using Reflex.Core;
using SmallBallBigPlane.Collectables;
using SmallBallBigPlane.Infrastructure.FSM;
using SmallBallBigPlane.Infrastructure.FSM.States;
using SmallBallBigPlane.Infrastructure.Services;
using SmallBallBigPlane.Infrastructure.Services.AssetManagement;
using SmallBallBigPlane.Infrastructure.Services.Factories;
using SmallBallBigPlane.UI.Windows;
using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.DI.Installers
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private WindowsSO windows;
        [SerializeField] private GameSettingsSO gameSettings;
        [SerializeField] private LevelsSO levels;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(JsonSerializer), typeof(ISerializer));
            containerBuilder.AddSingleton(typeof(SettingsSystem));
            containerBuilder.AddSingleton(typeof(GameManager));
            containerBuilder.AddSingleton(typeof(CoinManager));
            containerBuilder.AddSingleton(windows);
            containerBuilder.AddSingleton(gameSettings);
            containerBuilder.AddSingleton(levels);

            BindServices(containerBuilder);
            BindStates(containerBuilder);
            BindFactories(containerBuilder);
            BindAssets(containerBuilder);
            BindWindows(containerBuilder);
        }

        private void BindWindows(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(WindowsService));
        }

        private void BindAssets(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(AssetProvider));
        }

        private void BindFactories(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(UIFactory));
        }

        private void BindStates(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(BootstrapState));
            containerBuilder.AddSingleton(typeof(LoadGameState));
            containerBuilder.AddSingleton(typeof(MainMenuState));
            containerBuilder.AddSingleton(typeof(GameLoopState));
            containerBuilder.AddSingleton(typeof(ExitState));
            containerBuilder.AddSingleton(typeof(WinLevelState));
            containerBuilder.AddSingleton(typeof(LooseLevelState));
            containerBuilder.AddSingleton(typeof(LoadingState));

            containerBuilder.AddSingleton(c => new StateMachine(
                c.Resolve<BootstrapState>(),
                c.Resolve<LoadGameState>(),
                c.Resolve<MainMenuState>(),
                c.Resolve<GameLoopState>(),
                c.Resolve<ExitState>(),
                c.Resolve<WinLevelState>(),
                c.Resolve<LooseLevelState>(),
                c.Resolve<LoadingState>()
            ));
        }

        private void BindServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(typeof(FileDataService));
            containerBuilder.AddSingleton(typeof(SaveLoadSystem));
            containerBuilder.AddSingleton(typeof(SceneLoader));
        }
    }
}