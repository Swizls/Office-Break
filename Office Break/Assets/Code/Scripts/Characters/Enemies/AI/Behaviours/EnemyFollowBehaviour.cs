using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies.AI
{
    public class EnemyFollowBehaviour : EnemyBehaviour
    {
        private bool PlayerIsClose => Vector3.Distance(BehaviourController.transform.position, BehaviourController.PlayerPosition) < BehaviourController.GetComponent<AttackController>().AttackRange;

        public EnemyFollowBehaviour(EnemyBehaviourController controller, EnemyMover mover) : base(controller, mover)
        {
        }

        public override void Execute()
        {
            MoveAroundPlayer();

            if (PlayerIsClose)
                AttackController.PerformAttack();
        }

        private void MoveAroundPlayer()
        {
            Vector3 targetPosition = CalculatePointAroundPlayer(CIRCLE_RADIUS, ChosenAngle);

            Mover.SetDestination(targetPosition);
        }
    }

}