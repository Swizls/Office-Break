using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies.AI
{
    public class EnemyAttackBehaviour : EnemyBehaviour
    {
        private EnemyAttackController _attackController;

        public EnemyAttackBehaviour(Transform playerTransform, EnemyMover mover, EnemyAttackController attackController) : base(playerTransform, mover)
        {
            _attackController = attackController;
        }

        public override void Execute()
        {
            Mover.SetDestination(PlayerTransform.position);
        }
    }
}