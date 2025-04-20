using FabroGames.Characters.Animations;
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
        [SerializeField][Range(0f, 1f)] private float _cooldownReductionTime;

        protected AudioSource _audioSource;

        public Action AttackPerformed;
        public Action AlternativeAttackPerformed;

        private float CooldownReductionTime => _cooldownReductionTime;

        protected float Damage => _damage;
        protected float AttackForce => _attackForce;
        protected float LeftHandCooldownTime { get; private set; }
        protected float RightHandCooldownTime { get; private set; }
        protected bool IsAbleToAttackLeftHand { get; set; }
        protected bool IsAbleToAttackRightHand { get; set; }

        public abstract bool IsBlocking { get; protected set; }
        public bool IsAbleToAttack => IsAbleToAttackLeftHand && IsAbleToAttackRightHand;
        public float AttackRange => _attackRange;

        #region MONO

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            var animatorController = GetComponentInChildren<AnimatorController>();

            LeftHandCooldownTime = animatorController.PrimaryAttackAnimationLength - CooldownReductionTime;
            RightHandCooldownTime = animatorController.SecondaryAttackAnimationLength - CooldownReductionTime;

            IsAbleToAttackLeftHand = true;
            IsAbleToAttackRightHand = true;
        }

        #endregion

        protected abstract void PrimaryAttack();

        protected abstract void AlternativeAttack();

        protected void PlayAttackSFX()
        {
            _audioSource.pitch = UnityEngine.Random.Range(0.5f, 1.5f);
            _audioSource.Play();
        }

        protected IEnumerator CooldownTimer(AttackType attackType, float cooldownTimer)
        {
            switch (attackType)
            {
                case AttackType.LeftHand:
                    IsAbleToAttackLeftHand = false;
                    break;
                case AttackType.RightHand:
                    IsAbleToAttackRightHand = false;
                    break;
            }

            while (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            switch (attackType)
            {
                case AttackType.LeftHand:
                    IsAbleToAttackLeftHand = true;
                    break;
                case AttackType.RightHand:
                    IsAbleToAttackRightHand = true;
                    break;
            }
        }
    }
}