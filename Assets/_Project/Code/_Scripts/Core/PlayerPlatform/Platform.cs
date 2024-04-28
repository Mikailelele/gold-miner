using _Project.Core.Level;
using UnityEngine;
using VContainer;

namespace _Project.Core.PlayerPlatform
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Joint2D), typeof(PlatformMovementController))]
    public sealed class Platform : MonoBehaviour
    {
        [field: SerializeField] 
        public Joint2D Joint { get; private set; }
        
        [field: SerializeField]
        public PlatformMovementController MovementController { get; private set; }
        
        [SerializeField] 
        private HealthController _healthController;

        private ILevelService _levelService;

        [Inject]
        private void Construct(ILevelService levelService)
        {
            _levelService = levelService;
            
            this.LogInjectSuccess();
        }

        private void Awake()
        {
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void OnDie()
        {
            _levelService.RestartLevel();
        }

        private void SubscribeEvents()
        {
            _healthController.OnDieAction += OnDie;
        }
        
        private void UnsubscribeEvents()
        {
            _healthController.OnDieAction -= OnDie;
        }
    }
}