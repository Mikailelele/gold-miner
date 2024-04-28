using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace _Project.Utils
{
    public static class MapExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetValue<TKey, TValue>(
            this List<KeyValueMap<TKey, TValue>> keyValueMaps, in TKey key)
        {
            KeyValueMap<TKey,TValue> keyValueMap;
            for (int i = 0, count = keyValueMaps.Count; i < count; i++)
            {
                keyValueMap = keyValueMaps[i];
                if (keyValueMap.Key.Equals(key))
                {
                    return keyValueMap.Value;
                }
            }
            throw new KeyNotFoundException($"The key '{key}' was not found in the list.");
        }
    }
}