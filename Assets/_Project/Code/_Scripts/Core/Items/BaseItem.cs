using UnityEngine;

namespace _Project.Core.Items
{
    public enum EItemType
    {
        Coin,
    }
    
    public interface IItem
    {
        EItemType Type { get; }
        int Amount { get; }
        
        void Collect();
    }
    
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    public abstract class BaseItem : MonoBehaviour, IItem
    {
        [field: SerializeField]
        public EItemType Type { get; private set; }

        [field: Min(1)]
        [field: SerializeField]
        public int Amount { get; private set; }

        void IItem.Collect()
        {
            Destroy(gameObject);
        }
    }
}