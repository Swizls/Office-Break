using UnityEngine;
using System;

namespace OfficeBreak.Core.DamageSystem
{
    [Serializable]
    public class Health
    {
        [SerializeField] private float _initialHealth;

        private float _health;

        public event Action ValueChanged;
        public event Action Died;

        public float Value => _health;
        public float LeftHealthPercentage => _health / _initialHealth * 100;
        public bool IsDead => _health <= 0;

        private void Die() => Died?.Invoke();

        public void Initialize() => _health = _initialHealth;

        public void TakeDamage(float damage)
        {
            if (_health <= 0)
                return;

            if (damage < 0)
                throw new ArgumentException("Wrong damage value");

            _health -= damage;

            if (_health <= 0)
                Die();

            ValueChanged?.Invoke();
        }

        public void TakeHeal(float healPoints)
        {
            if (healPoints < 0)
                throw new ArgumentException("Wrong heal value");

            _health += healPoints;

            if(_health > _initialHealth)
                _health = _initialHealth;

            ValueChanged?.Invoke();
        }
    }
}