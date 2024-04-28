using DG.Tweening;
using UnityEngine;

namespace _Project.Core.Obstacles.Animations
{
    [DisallowMultipleComponent]
    public sealed class FIreAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _visual;
        
        [Space(10)]
        [SerializeField] private float _minScale = .5f;
        [SerializeField] private float _maxScale = 1f;
        [SerializeField] private float _minDuration = 1f;
        [SerializeField] private float _maxDuration = 2f;
        
        [Space(10)]
        [SerializeField] private Ease _ease = Ease.InOutSine;
        
        private Tween _tween;
        
        private void Start()
        {
            float scale = Random.Range(_minScale, _maxScale);
            float duration = Random.Range(_minDuration, _maxDuration);
            
            _tween = _visual.DOScale(scale, duration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(_ease);
        }
        
        private void OnDestroy()
        {
            _tween.Kill();
        }
    }
}