using _Project.Data;
using UnityEngine;

namespace _Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlatformSettings", menuName = "_Project/Data/PlatformSettings")]
    public sealed class PlatformSettingsSo : ScriptableObject
    {
        [field: SerializeField]
        public PlatformData Data { get; private set; }
    }
}