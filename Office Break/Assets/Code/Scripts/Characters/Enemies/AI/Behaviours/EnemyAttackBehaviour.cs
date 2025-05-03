using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies.AI
{
    public class EnemyAttackBehaviour : EnemyBehaviour
    {
        private readonly float _attackDistance;

        private bool IsEnoughCloseToAttack => Vector3.Distance(BehaviourController.PlayerPosition, EnemyTransform.position) < AttackController.AttackRange;

        public EnemyAttackBehaviour(EnemyBehaviourController controller, EnemyMover mover) : base(controller, mover)
        {
            _attackDistance = AttackController.AttackRange;
        }

        public override void Execute()
        {
            MoveTowardsPlayer();
            Attack();
        }

        private void Attack()
        {
            if(IsEnoughCloseToAttack)
                AttackController.PerformAttack();
        }

        private void MoveTowardsPlayer()
        {
            if (Vector3.Distance(BehaviourController.transform.position, BehaviourController.PlayerPosition) > _attackDistance)
                Mover.SetDestination(CalculatePointAroundPlayer(_attackDistance / 1.5f, ChosenAngle));
        }
    }
}