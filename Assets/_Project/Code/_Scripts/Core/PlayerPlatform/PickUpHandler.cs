using _Project.Core.Items;
using UnityEngine;
using VContainer;

namespace _Project.Core.PlayerPlatform
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    public sealed class PickUpHandler : MonoBehaviour
    {
        private IItem _item;
        
        private IItemsController _itemsController;
        
        [Inject]
        private void Construct(IItemsController itemsController)
        {
            _itemsController = itemsController;
            
            this.LogInjectSuccess();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out _item))
            {
                _itemsController.AddItem(_item);
                _item.Collect();
            }
        }
    }
}