using System;
using UnityEngine;

namespace OfficeBreak.Characters.FightingSystem
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class AttackController : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField][Range(0.2f, 5f)] private float _attackRange;
        [SerializeField] private float _attackForce;
        [SerializeField][Range(0f, 1f)] private float _cooldownReductionTime;

        protected AudioSource _audioSource;

        public Action AttackPerformed;
        public Action AlternativeAttackPerformed;

        protected float Damage => _damage;
        protected float AttackRange => _attackRange;
        protected float AttackForce => _attackForce;
        protected float CooldownReductionTime => _cooldownReductionTime;

        public abstract bool IsBlocking { get; protected set; }

        #region MONO

        private void Awake() => _audioSource = GetComponent<AudioSource>();

        #endregion

        protected abstract void PrimaryAttack();

        protected abstract void AlternativeAttack();

        protected void PlayAttackSFX()
        {
            _audioSource.pitch = UnityEngine.Random.Range(0.5f, 1.5f);
            _audioSource.Play();
        }
    }
}