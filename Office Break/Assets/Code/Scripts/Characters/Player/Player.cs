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
        private Transform _cameraTransform;

        public event Action<IHitable> GotHit;

        public Health Health => _health;

        private void Awake()
        {
            _health.Initialize();
            _cameraTransform = Camera.main.transform;
            _shaker = GetComponentInChildren<CameraShaker>();  
            _playerAttackController = GetComponent<PlayerAttackController>();
        }

        public void TakeHit(HitData hitData)
        {
            if (_playerAttackController.IsBlocking)
            {
                float hitAngle = Vector3.Angle(_cameraTransform.forward, hitData.HitDirection);
                if (hitAngle > PlayerAttackController.BLOCKING_ANGLE)
                    return;
            }

            _health.TakeDamage(hitData.Damage);
            _shaker.StartShake();

            GotHit?.Invoke(this);
        }
    }
}