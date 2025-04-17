using FabroGames.Player.Movement;
using OfficeBreak.Player;
using System;
using UnityEngine;

namespace FabroGames.Player
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        private const string MOVE_X = "moveX";
        private const string MOVE_Y = "moveY";
        private const string IS_FLYING = "IsFlying";
        private const string IS_RUNNING = "IsRunning";
        private const string IS_SLIDING = "IsSliding";
        private const string ATTACK = "Attack";
        private const string ALTERNATIVE_ATTACK = "AltAttack";
        private float ANIMATION_CHANGE_SPEED = 0.1f;

        [SerializeField] private FPSMovement _playerMovement;
        [SerializeField] private PlayerAttack _playerAttack;

        private Animator _animator;

        #region MONO
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _playerAttack.AttackPerformed += OnAttackPerform;
            _playerAttack.AlternativeAttackPerformed += OnAlternativeAttackPerform;
        }

        private void OnDisable()
        {
            _playerAttack.AttackPerformed -= OnAttackPerform;
            _playerAttack.AlternativeAttackPerformed -= OnAlternativeAttackPerform;
        }

        private void Update()
        {
            SetMovementDirection();
            SetFlyingBool();
            SetIsRunningBool();
            SetIsSlidingBool();
        }
        #endregion

        private void OnAttackPerform()
        {
            _animator.SetTrigger(ATTACK);
        }

        private void OnAlternativeAttackPerform()
        {
            _animator.SetTrigger(ALTERNATIVE_ATTACK);
        }

        private void SetIsSlidingBool()
        {
            _animator.SetBool(IS_SLIDING, _playerMovement.IsSliding);
        }

        private void SetIsRunningBool()
        {
            _animator.SetBool(IS_RUNNING, _playerMovement.IsRunning);
        }

        private void SetFlyingBool()
        {
            _animator.SetBool(IS_FLYING, _playerMovement.IsFlying);
        }

        private void SetMovementDirection()
        {
            _animator.SetFloat(MOVE_X, Mathf.MoveTowards(_animator.GetFloat(MOVE_X), _playerMovement.InputDirection.x, ANIMATION_CHANGE_SPEED));
            _animator.SetFloat(MOVE_Y, Mathf.MoveTowards(_animator.GetFloat(MOVE_Y), _playerMovement.InputDirection.y, ANIMATION_CHANGE_SPEED));
        }
    }
}