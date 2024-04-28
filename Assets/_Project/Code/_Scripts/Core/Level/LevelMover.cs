using System;
using _Project.ScriptableObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Core.Level
{
    public interface ILevelMover
    {
        void StartMovement();
        void StopMovement();
    }
    
    public sealed class LevelMover : ILevelMover, IDisposable
    {
        private ILevelService _levelService;
        
        private Transform _levelTransform;

        private Vector3 _movementDirection;
        private float _movementSpeed;
        private bool _isMoving;
        
        [Inject]
        private void Construct(ILevelService levelService, LevelsSettingsSo levelsSettingsSo)
        {
            _levelService = levelService;
            _movementSpeed = levelsSettingsSo.MovementSpeed;
            
            _movementDirection = new Vector3(0, _movementSpeed); 
            
            SubscribeEvents();
            
            this.LogInjectSuccess();
        }
        
        private void Init()
        {
            _levelTransform = _levelService.CurrentLevel.transform;
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
            
            StopMovement();
        }
        
        public void StartMovement()
        {
            MoveLevel().Forget();
        }

        public void StopMovement()
        {
            _isMoving = false;
        }

        private async UniTaskVoid MoveLevel()
        {
            _isMoving = true;
            
            while (_isMoving)
            {
                _levelTransform.position += _movementDirection * Time.deltaTime;
                await UniTask.Yield();
            }
        }

        private void SubscribeEvents()
        {
            _levelService.OnLevelLoadedAction += Init;
        }
        
        private void UnsubscribeEvents()
        {
            _levelService.OnLevelLoadedAction -= Init;
        }
    }
}