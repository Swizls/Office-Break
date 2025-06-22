using OfficeBreak.Characters.Animations;
using OfficeBreak.Core;
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

        private bool _isAbleToAttack = true;

        protected AudioSource _audioSource;
        protected AnimatorController _animatorController;

        private SFXPlayer _sfxPlayer;

        public Action AttackPerformed;
        public Action AlternativeAttackPerformed;

        public Action<bool> BlockStateChanged;

        protected float Damage => _damage;
        protected float AttackForce => _attackForce;
        public bool IsAbleToAttack 
        {
            get => _isAbleToAttack && !IsBlocking;
            protected set => _isAbleToAttack = value;
        }

        public abstract bool IsBlocking { get; protected set; }
        public HitData.AttackDirections BlockDirection { get; protected set; }
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
        }

        protected void OnAttackAnimationEnd()
        {
            IsAbleToAttack = true;
            _animatorController.AttackAnimationEnded -= OnAttackAnimationEnd;
        }

        protected void PlayAttackSFX() => _sfxPlayer.Play(nameof(_attackSFX));
    }
}