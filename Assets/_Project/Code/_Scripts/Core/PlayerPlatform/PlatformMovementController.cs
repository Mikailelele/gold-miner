using _Project.Core.Input;
using _Project.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Core.PlayerPlatform
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlatformMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private IInputService _inputService;
        
        private int _turningSpeed;
        private bool _isMoving;
        
        [Inject]
        private void Construct(IInputService inputService, PlatformSettingsSo platformData)
        {
            _inputService = inputService;
            _turningSpeed = platformData.Data.TurningSpeed;
            
            this.LogInjectSuccess();
        }
        
        private void OnDestroy()
        {
            StopMovement();
        }

        public void StartMovement()
        {
            StartMovementTask().Forget();
        }
        
        public void StopMovement()
        {
            _isMoving = false;
        }

        private async UniTaskVoid StartMovementTask()
        {
            _isMoving = true;
            
            while (_isMoving)
            {
                _rigidbody.AddForce(_inputService.MoveDirection * _turningSpeed * Time.deltaTime, ForceMode2D.Impulse);
                await UniTask.Yield();
            }
        }
    }
}