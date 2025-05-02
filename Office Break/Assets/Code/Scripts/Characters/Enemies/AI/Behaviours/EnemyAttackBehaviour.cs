using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies.AI
{
    public class EnemyAttackBehaviour : EnemyBehaviour
    {
        private const float MIN_COOLDOWN_TIME = 0.5f;
        private const float MAX_COOLDOWN_TIME = 3f;

        private EnemyAttackController _attackController;
        private float _cooldownTime = 0f;

        private bool IsEnoughCloseToAttack => Vector3.Distance(PlayerTransform.position, EnemyTransform.position) < _attackController.AttackRange;

        public EnemyAttackBehaviour(Transform playerTransform, EnemyMover mover, EnemyAttackController attackController) : base(playerTransform, mover)
        {
            _attackController = attackController;
        }

        public override void Execute()
        {
            UpdateCooldownTime();
            Mover.SetDestination(PlayerTransform.position);

            if (IsEnoughCloseToAttack && _cooldownTime <= 0)
            {
                _cooldownTime = UnityEngine.Random.Range(MIN_COOLDOWN_TIME, MAX_COOLDOWN_TIME);
                _attackController.PerformAttack();
            }
        }

        private void UpdateCooldownTime()
        {
            if(_cooldownTime > 0)
                _cooldownTime -= Time.deltaTime;
        }
    }
}