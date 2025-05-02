using OfficeBreak.Characters.FightingSystem;
using OfficeBreak.Core.DamageSystem;
using System;
using UnityEngine;

namespace OfficeBreak.Characters
{
    public class Player : MonoBehaviour, IHitable
    {
        [SerializeField] private Health _health;

        private CameraShaker _shaker;
        private PlayerAttackController _playerAttackController;

        public event Action<IHitable> GotHit;

        public Health Health => _health;

        private void Awake()
        {
            _health.Initialize();
            _shaker = GetComponentInChildren<CameraShaker>();  
            _playerAttackController = GetComponent<PlayerAttackController>();
        }

        public void TakeHit(HitData hitData)
        {
            if (_playerAttackController.IsBlocking)
                return;

            _health.TakeDamage(hitData.Damage);
            _shaker.StartShake();

            GotHit?.Invoke(this);
        }
    }
}