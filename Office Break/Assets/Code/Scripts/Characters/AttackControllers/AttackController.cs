using OfficeBreak.Characters.Animations;
using System;
using System.Collections;
using UnityEngine;

namespace OfficeBreak.Characters.FightingSystem
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class AttackController : MonoBehaviour
    {
        public enum AttackType
        {
            RightHand,
            LeftHand
        }

        [SerializeField] private float _damage;
        [SerializeField][Range(0.2f, 5f)] private float _attackRange;
        [SerializeField] private float _attackForce;

        protected AudioSource _audioSource;
        protected AnimatorController _animatorController;

        public Action AttackPerformed;
        public Action AlternativeAttackPerformed;
        public Action<bool> BlockStateChanged;

        protected float Damage => _damage;
        protected float AttackForce => _attackForce;
        protected bool IsAbleToAttackLeftHand { get; set; }
        protected bool IsAbleToAttackRightHand { get; set; }

        public abstract bool IsBlocking { get; protected set; }
        public bool IsAbleToAttack => IsAbleToAttackLeftHand && IsAbleToAttackRightHand && !IsBlocking;
        public float AttackRange => _attackRange;

        #region MONO

        private void Awake() => Initialize();

        #endregion

        protected void Initialize()
        {
            _audioSource = GetComponent<AudioSource>();

            _animatorController = GetComponentInChildren<AnimatorController>();

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

        protected abstract void PrimaryAttack();

        protected abstract void AlternativeAttack();

        protected abstract void FistAttack();
    }
}