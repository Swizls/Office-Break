using UnityEngine;
using System;

namespace OfficeBreak
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _initialHealth;

        private float _health;

        public Action Died;

        public float Value => _health;
        public float LeftHealthPercentage => _health / _initialHealth * 100;

        private void Awake() => _health = _initialHealth;

        public void TakeDamage(float damage)
        {
            if (_health <= 0)
                return;

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