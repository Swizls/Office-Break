using FabroGames.PlayerControlls;
using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Animations
{
    public class PlayerAnimatorController : AnimatorController
    {
        private const string IS_SLIDING = "IsSliding";

        [Header("Dependencies")]
        [SerializeField] private FPSMovement _playerMovement;
        [SerializeField] private PlayerAttackController _playerAttack;

        #region MONO
        private void Update()
        {
            SetMovementDirection(_playerMovement.InputDirection);
            SetIsSlidingBool();
            SetIsRunningBool();
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