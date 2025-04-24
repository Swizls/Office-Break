using FabroGames.PlayerControlls;
using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Animations
{
    public class PlayerAnimatorController : AnimatorController
    {
        [Header("Dependencies")]
        [SerializeField] private PlayerAttackController _playerAttack;
        private IMovable _playerMovement;

        #region MONO

        private void Awake()
        {
            _playerMovement = GetComponentInParent<IMovable>();
        }

        private void Update()
        {
            Vector3 movementVector = _playerMovement.IsMoving ? _playerMovement.Velocity.normalized : Vector3.zero;
            SetMovementDirection(CalculateRealitveMovementDirection(movementVector, transform.forward));
            SetIsRunningBool();
        }

        private void OnEnable()
        {
            _playerAttack.AttackPerformed += OnAttackPerform;
            _playerAttack.AlternativeAttackPerformed += OnAlternativeAttackPerform;
            _playerAttack.BlockStateChanged += OnBlockStateChange;
        }

        private void OnDisable()
        {
            _playerAttack.AttackPerformed -= OnAttackPerform;
            _playerAttack.AlternativeAttackPerformed -= OnAlternativeAttackPerform;
            _playerAttack.BlockStateChanged -= OnBlockStateChange;
        }
        #endregion

        protected override void SetFlyingBool()
        {
            _animator.SetBool(IS_FLYING, !_playerMovement.IsGrounded);
        }

        protected override void SetIsRunningBool()
        {
            _animator.SetBool(IS_RUNNING, _playerMovement.IsRunning);
        }
    }
}