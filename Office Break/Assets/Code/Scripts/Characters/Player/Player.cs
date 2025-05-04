using OfficeBreak.Characters.FightingSystem;
using OfficeBreak.Core;
using System;
using UnityEngine;

namespace OfficeBreak.Characters
{
    [RequireComponent(typeof(AudioSource))]
    public class Player : MonoBehaviour, IHitable
    {
        [SerializeField] private Health _health;
        [SerializeField] private AudioClip[] _hitSFX;

        private CameraShaker _shaker;
        private PlayerAttackController _playerAttackController;
        private Transform _cameraTransform;
        private SFXPlayer _sfxPlayer;

        public event Action<IHitable> GotHit;

        public Health Health => _health;

        private void Awake()
        {
            _health.Initialize();
            _cameraTransform = Camera.main.transform;
            _shaker = GetComponentInChildren<CameraShaker>();
            _playerAttackController = GetComponent<PlayerAttackController>();

            _sfxPlayer = new SFXPlayer(GetComponent<AudioSource>());
            _sfxPlayer.AddClips(nameof(_hitSFX), _hitSFX);
        }

        public void TakeHit(HitData hitData)
        {
            if (_playerAttackController.IsHitBlocked(hitData))
            {
                return;
            }

            _health.TakeDamage(hitData.Damage);
            _shaker.StartShake();
            _sfxPlayer.Play(nameof(_hitSFX));

            GotHit?.Invoke(this);
        }
    }
}