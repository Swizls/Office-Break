using OfficeBreak.Characters.Enemies;
using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace FabroGames.Characters.Animations
{
    public class EnemyAnimatorController : AnimatorController
    {
        [Header("Dependencies")]
        [SerializeField] private EnemyMover _enemyMover;
        [SerializeField] private AttackController _attackController;

        #region MONO

        private void Update()
        {
            SetIsRunningBool();
            SetMovementDirection(CalculateRealitveMovementDirection(_enemyMover.AgentVelocity, _enemyMover.transform.forward));
        }

        private void OnEnable()
        {
            _attackController.AttackPerformed += OnAttackPerform;
            _attackController.AlternativeAttackPerformed += OnAlternativeAttackPerform;
        }

        private void OnDisable()
        {
            _attackController.AttackPerformed -= OnAttackPerform;
            _attackController.AlternativeAttackPerformed -= OnAlternativeAttackPerform;
        }

        #endregion

        protected override void SetFlyingBool() => throw new System.NotImplementedException();

        protected override void SetIsRunningBool() => _animator.SetBool(IS_RUNNING, _enemyMover.IsRunning);
    }
}