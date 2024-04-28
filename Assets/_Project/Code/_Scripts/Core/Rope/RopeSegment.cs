using UnityEngine;

namespace _Project.Core.Rope
{
    [DisallowMultipleComponent]
    public sealed class RopeSegment : MonoBehaviour
    {
        [field: SerializeField] 
        public Rigidbody2D Rigidbody;
        
        [field: SerializeField] 
        public HingeJoint2D Joint;
        
        [field: SerializeField] 
        public SpriteRenderer SpriteRenderer;
        
        [Range(0.5f, 1.5f)]
        [SerializeField] 
        private float _spaceBetweenSegments = 1f;
        
        private GameObject _connectedAbove;
        
        public GameObject ConnectedBelow { get; set; }
        
        public void ResetAnchor()
        {
            _connectedAbove = Joint.connectedBody.gameObject;
            if (_connectedAbove != null && _connectedAbove.TryGetComponent(out RopeSegment aboveSegment))
            {
                aboveSegment.ConnectedBelow = gameObject;
                
                float spriteBottom = aboveSegment.SpriteRenderer.bounds.size.y;
                Joint.connectedAnchor = new Vector2(0, spriteBottom * -_spaceBetweenSegments);
            }
            else
            {
                Joint.connectedAnchor = Vector2.zero;
            }
        }
    }
}