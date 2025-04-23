using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies.AI
{
    public class EnemyAttackBehaviour : EnemyBehaviour
    {
        private EnemyAttackController _attackController;

        private bool IsEnoughCloseToAttack => Vector3.Distance(PlayerTransform.position, EnemyTransform.position) < _attackController.AttackRange;

        public EnemyAttackBehaviour(Transform playerTransform, EnemyMover mover, EnemyAttackController attackController) : base(playerTransform, mover)
        {
            _attackController = attackController;
        }

        public override void Execute()
        {
            Mover.SetDestination(PlayerTransform.position);

            if (IsEnoughCloseToAttack)
                _attackController.PerformAttack();
        }
    }
}