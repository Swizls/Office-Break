using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies.AI
{
    public class EnemyAttackBehaviour : EnemyBehaviour
    {
        private const float MIN_COOLDOWN_TIME = 0.5f;
        private const float MAX_COOLDOWN_TIME = 3f;

        private readonly float _attackDistance;

        private float _cooldownTime = 0f;

        private bool IsEnoughCloseToAttack => Vector3.Distance(BehaviourController.PlayerPosition, EnemyTransform.position) < AttackController.AttackRange;

        public EnemyAttackBehaviour(EnemyBehaviourController controller, EnemyMover mover) : base(controller, mover)
        {
            _attackDistance = AttackController.AttackRange;
        }

        public override void Execute()
        {
            UpdateCooldownTime();
            MoveTowardsPlayer();
            Attack();
        }

        private void Attack()
        {
            if (IsEnoughCloseToAttack && _cooldownTime <= 0)
            {
                _cooldownTime = UnityEngine.Random.Range(MIN_COOLDOWN_TIME, MAX_COOLDOWN_TIME);
                AttackController.PerformAttack();
            }
        }

        private void MoveTowardsPlayer()
        {
            if (Vector3.Distance(BehaviourController.transform.position, BehaviourController.PlayerPosition) > _attackDistance)
                Mover.SetDestination(CalculatePointAroundPlayer(_attackDistance / 1.5f, ChosenAngle));
        }

        private void UpdateCooldownTime()
        {
            if(_cooldownTime > 0)
                _cooldownTime -= Time.deltaTime;
        }
    }
}