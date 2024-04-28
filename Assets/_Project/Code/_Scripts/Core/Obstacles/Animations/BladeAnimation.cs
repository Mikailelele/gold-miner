using DG.Tweening;
using UnityEngine;

namespace _Project.Core.Obstacles.Animations
{
    [DisallowMultipleComponent]
    public sealed class BladeAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _visual;
        [SerializeField] private Vector3 _rotationValue;
        [SerializeField] private float _cycleDuration = 3;
        
        [Space(10)]
        [SerializeField] private Ease _ease = Ease.Linear;
        
        private Tween _tween;
        
        private void Start()
        {
            _tween = _visual.DORotate(_rotationValue, _cycleDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(_ease);
        }
        
        private void OnDestroy()
        {
            _tween.Kill();
        }
    }
}