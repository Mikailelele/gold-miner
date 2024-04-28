using UnityEngine;

namespace _Project.Data
{
    [System.Serializable]
    public struct LevelData
    {
        [field: SerializeField]
        public GameObject LevelPrefab { get; private set; }
    }
}