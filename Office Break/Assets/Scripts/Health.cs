using UnityEngine;
using System;

namespace OfficeBreak
{
    [System.Serializable]
    public class Health
    {
        [SerializeField] private float _health;
        public float Value => _health;

        public Action Died;

        public Health(float health)
        {
            _health = health;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentException("Wrong damage value");

            _health -= damage;

            if (_health <= 0)
                Die();
        }

        private void Die()
        {
            Died?.Invoke();
        }
    }
}