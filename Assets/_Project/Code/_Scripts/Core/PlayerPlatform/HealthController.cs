using System;
using _Project.Core.Interfaces;
using _Project.Data;
using _Project.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace _Project.Core.PlayerPlatform
{
    [DisallowMultipleComponent]
    public sealed class HealthController : MonoBehaviour, IDamageable
    {
        public int MaxHealth { get; private set; }
        public int MinHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        
        public event Action OnDieAction = delegate { };
        
        [Inject]
        private void Construct(PlatformSettingsSo platformSettingsSo)
        {
            PlatformData data = platformSettingsSo.Data;
            MaxHealth = data.MaxHealth;
            MinHealth = data.MinHealth;
            CurrentHealth = MaxHealth;
            
            this.LogInjectSuccess();
        }
        
        void IDamageable.TakeDamage(in int damage)
        {
            int newHealth = Mathf.Clamp(CurrentHealth -= damage, MinHealth, MaxHealth);
            CurrentHealth = newHealth;
            
            if (CurrentHealth <= MinHealth)
                OnDie();
        }
        
        private void OnDie()
        {
            OnDieAction.Invoke();
        }
    }
}