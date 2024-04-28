using System;
using _Project.Core.Level;
using _Project.Core.PlayerPlatform;
using VContainer;

namespace _Project.Core.Input
{
    public sealed class InputController : IDisposable
    {
        private IInputService _inputService;

        private PlatformMovementController _movementController;
        private ILevelMover _levelMover;
        
        [Inject]
        private void Construct(IInputService inputService, Platform platform, ILevelMover levelMover)
        {
            _inputService = inputService;
            _movementController = platform.MovementController;
            _levelMover = levelMover;
            
            SubscribeEvents();
            
            this.LogInjectSuccess();
        }
        
        void IDisposable.Dispose()
        {
            UnsubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _inputService.OnMovementStartedAction += _movementController.StartMovement;
            _inputService.OnMovementStoppedAction += _movementController.StopMovement;
            
            _inputService.OnTouchPerformedAction += _levelMover.StartMovement;
            _inputService.OnTouchCanceledAction += _levelMover.StopMovement;
        }
        
        private void UnsubscribeEvents()
        {
            _inputService.OnMovementStartedAction -= _movementController.StartMovement;
            _inputService.OnMovementStoppedAction -= _movementController.StopMovement;
            
            _inputService.OnTouchPerformedAction -= _levelMover.StartMovement;
            _inputService.OnTouchCanceledAction -= _levelMover.StopMovement;
        }
    }
}