using FabroGames.Player.Movement;
using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace FabroGames.Characters.Animations
{
    public class PlayerAnimatorController : AnimatorController
    {
        private const string IS_SLIDING = "IsSliding";

        [SerializeField] private FPSMovement _playerMovement;
        [SerializeField] private PlayerAttackController _playerAttack;

        #region MONO
        private void Update()
        {
            SetMovementDirection(_playerMovement.InputDirection);
            SetIsSlidingBool();
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
        #endregion

        protected override void SetFlyingBool()
        {
            _animator.SetBool(IS_FLYING, _playerMovement.IsFlying);
        }

        protected override void OnAttackPerform()
        {
            _animator.SetTrigger(ATTACK);
        }

        protected override void OnAlternativeAttackPerform()
        {
            _animator.SetTrigger(ALTERNATIVE_ATTACK);
        }

        protected override void SetIsRunningBool()
        {
            _animator.SetBool(IS_RUNNING, _playerMovement.IsRunning);
        }
        private void SetIsSlidingBool()
        {
            _animator.SetBool(IS_SLIDING, _playerMovement.IsSliding);
        }
    }
}