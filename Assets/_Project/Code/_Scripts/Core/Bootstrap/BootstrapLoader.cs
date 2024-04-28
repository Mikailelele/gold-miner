using _Project.Core.Messages;
using _Project.Core.UI;
using _Project.Utils;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace _Project.Core.Bootstrap
{
    [DisallowMultipleComponent]
    public sealed class BootstrapLoader : MonoBehaviour
    {
        private IPublisher<OnUiActionMessage> _onUiActionPublisher;
        
        [Inject]
        private void Construct(IPublisher<OnUiActionMessage> onUiActionPublisher)
        {
            _onUiActionPublisher = onUiActionPublisher;
        }
        
        private async void Start()
        {
            await SceneManager.LoadSceneAsync(Constants.Scenes.Menu, LoadSceneMode.Additive);
            _onUiActionPublisher.Publish(new OnUiActionMessage(EUiType.MainMenu));
        }
    }
}