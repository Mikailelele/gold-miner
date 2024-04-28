using System;
using System.Collections.Generic;

namespace _Project.Core.Items
{
    public interface IItemsController
    {
        int Amount(in EItemType type);
        
        void AddItem(in IItem item);
        
        event Action<EItemType> OnItemCollectedAction;
    }
    
    public sealed class ItemsController : IItemsController
    {
        private readonly Dictionary<EItemType, int> _items = new ()
        {
            { EItemType.Coin, 0 },
        };
        
        public int Amount(in EItemType type) => _items[type];

        public event Action<EItemType> OnItemCollectedAction;
        
        void IItemsController.AddItem(in IItem item)
        {
            _items[item.Type] += item.Amount;
            
            OnItemCollectedAction?.Invoke(item.Type);
        }
    }
}