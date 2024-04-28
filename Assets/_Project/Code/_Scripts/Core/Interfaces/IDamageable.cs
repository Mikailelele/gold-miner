using System;

namespace _Project.Core.Interfaces
{
    public interface IDamageable
    {
        int MaxHealth { get; }
        int MinHealth { get; }
        int CurrentHealth { get; }
        
        void TakeDamage(in int damage);

        event Action OnDieAction;
    }
}