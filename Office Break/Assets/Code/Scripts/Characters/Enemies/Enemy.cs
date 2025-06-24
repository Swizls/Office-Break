using OfficeBreak.Characters.Enemies.AI;
using OfficeBreak.Characters.FightingSystem;
using OfficeBreak.Core;
using System;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies
{
    [RequireComponent(typeof(EnemyMover))]
    [RequireComponent(typeof(EnemyAttackController))]
    [RequireComponent(typeof (AudioSource))]
    public class Enemy : MonoBehaviour, IHitable
    {
        [SerializeField] private Health _health;
        [SerializeField] private AudioClip[] _hitSFX;

        private EnemyAttackController _attackController;
        private SFXPlayer _sfxPlayer;

        public event Action<HitData> GotHit;

        public Health Health => _health;
        public EnemyBehaviourController BehaviourController { get; private set; }

        #region MONO

        private void Awake()
        {
            _health.Initialize();

            _attackController = GetComponent<EnemyAttackController>();
            BehaviourController = GetComponent<EnemyBehaviourController>();

            _sfxPlayer = new SFXPlayer(GetComponent<AudioSource>());
            _sfxPlayer.AddClips(nameof(_hitSFX), _hitSFX);
        }

        #endregion

        public void Initialize(Player player)
        {
            _attackController.Initialize(player);
            BehaviourController.Initialize(player);
        }

        public void TakeHit(HitData hitData)
        {
            _health.TakeDamage(hitData.Damage);
            _sfxPlayer.Play(nameof(_hitSFX));
            GotHit?.Invoke(hitData);
        }
    }
}