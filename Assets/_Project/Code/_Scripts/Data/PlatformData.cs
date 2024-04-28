using UnityEngine;

namespace _Project.Data
{
    [System.Serializable]
    public struct PlatformData
    {
        [field: SerializeField]
        public int TurningSpeed { get; private set; }
        
        [field: SerializeField]
        public int RopeSegmentsCount { get; private set; }
        
        [field: Space(10)]
        [field: SerializeField]
        public int MaxHealth { get; private set; }
        
        [field: SerializeField]
        public int MinHealth { get; private set; }
    }
}