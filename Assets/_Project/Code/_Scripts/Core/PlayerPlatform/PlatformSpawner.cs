using _Project.Core.Rope;
using Cinemachine;
using UnityEngine;
using VContainer;

namespace _Project.Core.PlayerPlatform
{
    [DisallowMultipleComponent]
    public sealed class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;

        private Platform _platform;
        private PlatformRope _rope;
        
        [Inject]
        private void Construct(Platform platform, PlatformRope rope)
        {
            _platform = platform;
            _rope = rope;
            
            SubscribeEvents();
            
            this.LogInjectSuccess();
        }
        
        private void SpawnPlatform(GameObject bodyToConnect)
        {
            if(bodyToConnect.TryGetComponent(out Rigidbody2D body))
                _platform.Joint.connectedBody = body;
            
            Transform platformTransform = _platform.transform;
            _camera.Follow = platformTransform;
            _camera.LookAt = platformTransform;
            
            UnsubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _rope.OnRopeInitiatedAction += SpawnPlatform;
        }
        
        private void UnsubscribeEvents()
        {
            _rope.OnRopeInitiatedAction -= SpawnPlatform;
        }
    }
}