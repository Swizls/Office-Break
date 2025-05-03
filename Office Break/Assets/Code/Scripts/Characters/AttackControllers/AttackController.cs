using OfficeBreak.Characters.Animations;
using OfficeBreak.Core;
using OfficeBreak.Core.DamageSystem;
using System;
using System.Collections;
using UnityEngine;

namespace OfficeBreak.Characters.FightingSystem
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class AttackController : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField][Range(0.2f, 5f)] private float _attackRange;
        [SerializeField] private float _attackForce;
        [SerializeField] private AudioClip[] _attackSFX;

        protected AudioSource _audioSource;
        protected AnimatorController _animatorController;

        private SFXPlayer _sfxPlayer;

        public Action AttackPerformed;
        public Action AlternativeAttackPerformed;

        public Action<bool> BlockStateChanged;

        protected float Damage => _damage;
        protected float AttackForce => _attackForce;
        protected bool IsAbleToAttackLeftHand { get; set; }
        protected bool IsAbleToAttackRightHand { get; set; }

        public abstract bool IsBlocking { get; protected set; }
        public HitData.AttackDirections BlockDirection { get; protected set; }
        public bool IsAbleToAttack => IsAbleToAttackLeftHand && IsAbleToAttackRightHand && !IsBlocking;
        public float AttackRange => _attackRange;

        #region MONO

        private void Awake() => Initialize();

        #endregion

        protected void Initialize()
        {
            _audioSource = GetComponent<AudioSource>();
            _animatorController = GetComponentInChildren<AnimatorController>();

            _sfxPlayer = new SFXPlayer(_audioSource);
            _sfxPlayer.AddClips(nameof(_attackSFX), _attackSFX);

            IsAbleToAttackLeftHand = true;
            IsAbleToAttackRightHand = true;
        }

        protected void OnLeftAttackCooldownEnd()
        {
            IsAbleToAttackRightHand = true;
            IsAbleToAttackLeftHand = true;
            _animatorController.AttackAnimationEnded -= OnLeftAttackCooldownEnd;
            FistAttack();
        }

        protected void OnRightAttackCooldownEnd()
        {
            IsAbleToAttackRightHand = true;
            IsAbleToAttackLeftHand = true;
            _animatorController.AttackAnimationEnded -= OnRightAttackCooldownEnd;
            FistAttack();
        }

        protected void PlayAttackSFX() => _sfxPlayer.Play(nameof(_attackSFX));

        protected abstract void PrimaryAttack();

        protected abstract void AlternativeAttack();

        protected abstract void FistAttack();

    }
}