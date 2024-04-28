using _Project.Core.Messages;
using MessagePipe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace _Project.Core.UI
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public sealed class UiButton : MonoBehaviour, IPointerUpHandler
    {
        [SerializeField] private EUiType _type;
        
        private IPublisher<OnUiActionMessage> _publisher;

        private OnUiActionMessage _message;
        
        [Inject]
        private void Construct(IPublisher<OnUiActionMessage> publisher)
        {
            _publisher = publisher;
            
            _message = new OnUiActionMessage(_type);
            
            this.LogInjectSuccess();
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                _publisher.Publish(_message);
        }
    }
}