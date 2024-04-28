using System;
using UnityEngine;
using VContainer;

namespace _Project.Core.Items.UI
{  
    [DisallowMultipleComponent]
    public sealed class ItemsViewController : MonoBehaviour
    {
        private IItemsController _itemsController;
        
        [SerializeField] private CoinsView _coinsView;
        
        [Inject]
        private void Construct(IItemsController itemsController)
        {
            _itemsController = itemsController;
            
            SubscribeEvents();
            
            this.LogInjectSuccess();
        }

        public void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void UpdateItemsView(EItemType type)
        {
            switch (type)
            {
                case EItemType.Coin:
                    _coinsView.UpdateText(_itemsController.Amount(type));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        private void SubscribeEvents() => _itemsController.OnItemCollectedAction += UpdateItemsView;
        private void UnsubscribeEvents() => _itemsController.OnItemCollectedAction -= UpdateItemsView;
    }
}