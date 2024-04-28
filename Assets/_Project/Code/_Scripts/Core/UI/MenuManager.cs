using System;
using System.Collections.Generic;
using _Project.Core.Messages;
using _Project.Utils;
using MessagePipe;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace _Project.Core.UI
{
    public enum EUiType
    {
        MainMenu,
        StartGame,
        SettingsMenu,
        GameOverMenu,
    }
    
    [DisallowMultipleComponent]
    public sealed class MenuManager : MonoBehaviour
    {
        [SerializeField] private List<KeyValueMap<EUiType, GameObject>> _menuMap;

        private GameObject _currentMenu;
        private IDisposable _disposable;
        
        [Inject]
        private void Construct(ISubscriber<OnUiActionMessage> subscriber)
        {
            var bag = DisposableBag.CreateBuilder();
            
            subscriber.Subscribe(HandleUiAction).AddTo(bag);
            
            _disposable = bag.Build();
        }
        
        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
        
        private void HandleUiAction(OnUiActionMessage message)
        {
            if(message.Type == EUiType.StartGame)
            {
                SceneManager.LoadSceneAsync(Constants.Scenes.Gameplay);
                SceneManager.UnloadSceneAsync(Constants.Scenes.Menu);
                return;
            }
            
            if (_currentMenu != null)
                SetActiveMenu(_currentMenu, false);
            _currentMenu = _menuMap.GetValue(message.Type);
            
            SetActiveMenu(_currentMenu, true);
        }
        
        private void SetActiveMenu(in GameObject menu, in bool isActive)
        {
            menu.SetActive(isActive);
        }
    }
}