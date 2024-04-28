using System;
using _Project.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace _Project.Core.Rope
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class PlatformRope : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _hook;
        [SerializeField] private RopeSegment _ropeSegmentPrefab;
        
        private Transform _transform;
        private Rigidbody2D _previousSegmentRigidbody;
        private RopeSegment _firstSegment;
        private RopeSegment _lastSegment;
        
        private int _segmentsCount;
        
        public event Action<GameObject> OnRopeInitiatedAction;

        [Inject]
        private void Construct(PlatformSettingsSo platformSettingsSo)
        {
            _segmentsCount = platformSettingsSo.Data.RopeSegmentsCount;
            
            this.LogInjectSuccess();
        }
        
        private void Awake()
        {
            _transform = transform;
            _previousSegmentRigidbody = _hook;
        }

        private void Start()
        {
            InitRope();
        }

        public void AddSegment()
        {
            RopeSegment segment = GenerateRopeSegment();
            segment.Joint.connectedBody = _hook;
            segment.ConnectedBelow = _firstSegment.gameObject;
            _firstSegment.Joint.connectedBody = segment.Rigidbody;
            _firstSegment.ResetAnchor();
            _firstSegment = segment;
        }
        
        public void RemoveSegment()
        {
            RopeSegment newTopSegment = _firstSegment.ConnectedBelow.GetComponent<RopeSegment>();
            newTopSegment.Joint.connectedBody = _hook;
            newTopSegment.transform.position = _hook.transform.position;
            newTopSegment.ResetAnchor();
            Destroy(_firstSegment.gameObject);
            _firstSegment = newTopSegment;
        }

        private void InitRope()
        {
            RopeSegment segment;
            for (int i = 0; i < _segmentsCount; i++)
            {
                segment = GenerateRopeSegment();
                segment.Joint.connectedBody = _previousSegmentRigidbody;

                _previousSegmentRigidbody = segment.Rigidbody;

                segment.ResetAnchor();
                
                if (i == 0)
                    _firstSegment = segment;
                
                if (i == _segmentsCount - 1)
                {
                    _lastSegment = segment;
                    _lastSegment.SpriteRenderer.enabled = false;
                }
            }
            
            OnRopeInitiatedAction?.Invoke(_lastSegment.gameObject);
        }
        
        private RopeSegment GenerateRopeSegment()
        {
            RopeSegment segment = Instantiate(_ropeSegmentPrefab, _transform.position, Quaternion.identity);
            Transform segmentTransform = segment.transform;
            segmentTransform.SetParent(_transform);
            segmentTransform.position = _transform.position;
            return segment;
        }
    }
}