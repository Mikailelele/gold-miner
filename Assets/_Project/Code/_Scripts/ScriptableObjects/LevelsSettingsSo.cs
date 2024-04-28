using System.Collections.Generic;
using _Project.Data;
using UnityEngine;

namespace _Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelsSettings", menuName = "_Project/Data/LevelsSettings")]
    public sealed class LevelsSettingsSo : ScriptableObject
    {
        [field: SerializeField] 
        public List<LevelData> Data { get; private set; }
        
        [field: Range(0.5f, 5f)]
        [field: SerializeField]
        public float MovementSpeed { get; private set; }
    }
}