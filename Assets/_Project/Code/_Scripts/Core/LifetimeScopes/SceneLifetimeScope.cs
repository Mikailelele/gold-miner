using System.Runtime.CompilerServices;
using _Project.Core.Input;
using _Project.Core.Items;
using _Project.Core.Level;
using _Project.Core.PlayerPlatform;
using _Project.Core.Rope;
using _Project.ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Core.LifetimeScopes
{
    [DisallowMultipleComponent]
    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [Header("Prefabs")]
        [SerializeField] private Platform _platformPrefab;
        [SerializeField] private PlatformRope _ropePrefab;
        [SerializeField] private GameObject _levelSetuperPrefab;
        [SerializeField] private GameObject _uiPrefab;
        
        [Header("Scriptable Objects")]
        [SerializeField] private PlatformSettingsSo _platformSettingsSo;
        [SerializeField] private LevelsSettingsSo _levelsSettingsSo;
        
        private IContainerBuilder _builder;
        
        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;
            
            RegisterPlatformSettings();
            RegisterLevelsSettings();
            
            RegisterInputService();
            
            RegisterLevelService();
            RegisterItemsController();
            RegisterLevelSetuper();
            RegisterLevelMover();
            
            RegisterPlayerPlatform();
            RegisterRope();
            
            RegisterInputController();

            RegisterUi();
        }
        
        private void RegisterPlatformSettings()
        {
            _builder.RegisterInstance(_platformSettingsSo);
        }

        private void RegisterLevelsSettings()
        {
            _builder.RegisterInstance(_levelsSettingsSo);
        }

        private void RegisterInputService()
        {
            _builder.RegisterEntryPoint<InputService>()
                .As<IInputService>();
        }

        private void RegisterLevelService()
        {
            _builder.RegisterEntryPoint<LevelService>()
                .As<ILevelService>();
            
            Resolve<ILevelService>();
        }
        
        private void RegisterItemsController()
        {
            _builder.Register<ItemsController>(Lifetime.Singleton)
                .As<IItemsController>();
        }
        
        private void RegisterLevelSetuper()
        {
            GameObject instance = Instantiate(_levelSetuperPrefab);
            
            _builder.RegisterBuildCallback(
                container =>
                {
                    container.InjectGameObject(instance);
                });
        }
        
        private void RegisterLevelMover()
        {
            _builder.RegisterEntryPoint<LevelMover>()
                .As<ILevelMover>();
            
            Resolve<ILevelMover>();
        }
        
        private void RegisterPlayerPlatform()
        {
            _builder.RegisterComponentInNewPrefab(_platformPrefab, Lifetime.Singleton)
                .AsSelf();
            
            _builder.RegisterBuildCallback(
                container =>
                { 
                    Platform platform = container.Resolve<Platform>();
                    container.InjectGameObject(platform.gameObject);
                });        
        }
        
        private void RegisterRope()
        {
            _builder.RegisterComponentInNewPrefab(_ropePrefab, Lifetime.Singleton)
                .AsSelf();
            
            Resolve<PlatformRope>();
        }
        
        private void RegisterInputController()
        {
            _builder.RegisterEntryPoint<InputController>()
                .AsSelf();
            
            Resolve<InputController>();
        }
        
        private void RegisterUi()
        {
            GameObject instance = Instantiate(_uiPrefab);
            
            _builder.RegisterBuildCallback(
                container =>
                {
                    container.InjectGameObject(instance);
                });
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Resolve<T>()
        {
            _builder.RegisterBuildCallback(
                container =>
                { 
                    container.Resolve<T>();
                });
        }
    }
}