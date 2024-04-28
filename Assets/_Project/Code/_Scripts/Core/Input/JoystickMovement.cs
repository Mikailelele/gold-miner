using UnityEngine;
using VContainer;

namespace _Project.Core.Input
{
    [DisallowMultipleComponent]
    public sealed class JoystickMovement : MonoBehaviour
    {
        private IInputService _inputService;
        
        private Transform _transform;
        
        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
            
            SubscribeEvents();
            
            this.LogInjectSuccess();
        }
        
        private void Awake()
        {
            _transform = transform;
        }
        
        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
        
        private void SetPosition()
        {
            _transform.position = _inputService.TouchPosition;
        }
        
        private void SubscribeEvents()
        {
            _inputService.OnTouchPerformedAction += SetPosition;
        }
        
        private void UnsubscribeEvents()
        {
            _inputService.OnTouchPerformedAction -= SetPosition;
        }
    }
}