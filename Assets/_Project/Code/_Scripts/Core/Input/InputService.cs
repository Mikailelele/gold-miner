using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace _Project.Core.Input
{
    public interface IInputService
    {
        Vector2 MoveDirection { get; }
        Vector2 TouchPosition { get; }
        
        event Action OnMovementStartedAction;
        event Action OnMovementStoppedAction;
        
        event Action OnTouchPerformedAction;
        event Action OnTouchCanceledAction;
    }
    
    public sealed class InputService : IInputService, IInitializable, IDisposable
    {
        private ProjectInput _input;

        public Vector2 MoveDirection => _input.Player.MoveX.ReadValue<Vector2>();
        public Vector2 TouchPosition => _input.Player.TouchValue.ReadValue<Vector2>();

        public event Action OnMovementStartedAction = delegate { };
        public event Action OnMovementStoppedAction = delegate { };
        
        public event Action OnTouchPerformedAction = delegate { };
        public event Action OnTouchCanceledAction = delegate { };
        
        void IInitializable.Initialize()
        {
            _input = new ProjectInput();
            _input.Enable();
            
            SubscribeEvents();
        }

        void IDisposable.Dispose()
        {
            _input.Disable(); 
            UnsubscribeEvents();
        }
        
        private void OnMovementStarted(InputAction.CallbackContext ctx)
        {
            OnMovementStartedAction.Invoke();
        }
        
        private void OnMovementCanceled(InputAction.CallbackContext ctx)
        {
            OnMovementStoppedAction.Invoke();
        }
        
        private void OnTouchPerformed(InputAction.CallbackContext ctx)
        {
            OnTouchPerformedAction.Invoke();
        }
        
        private void OnTouchCanceled(InputAction.CallbackContext ctx)
        {
            OnTouchCanceledAction.Invoke();
        }
        
        private void SubscribeEvents()
        {
            _input.Player.MoveX.started += OnMovementStarted;
            _input.Player.MoveX.canceled += OnMovementCanceled;
            
            _input.Player.TouchButton.performed += OnTouchPerformed;
            _input.Player.TouchButton.canceled += OnTouchCanceled;
        }
        
        private void UnsubscribeEvents()
        {
            _input.Player.MoveX.started -= OnMovementStarted;
            _input.Player.MoveX.canceled -= OnMovementCanceled;
            
            _input.Player.TouchButton.performed -= OnTouchPerformed;
            _input.Player.TouchButton.canceled -= OnTouchCanceled;
        }
    }
}