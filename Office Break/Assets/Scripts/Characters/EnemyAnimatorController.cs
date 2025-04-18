using OfficeBreak.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace FabroGames.Player
{
    public class EnemyAnimatorController : AnimatorController
    {
        [SerializeField] private EnemyMover _enemyMover;

        private void Update()
        {
            Vector2 movementDirection = new Vector2(_enemyMover.AgentVelocity.normalized.x, _enemyMover.AgentVelocity.normalized.z);
            SetMovementDirection(movementDirection);
        }

        protected override void OnAlternativeAttackPerform()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnAttackPerform()
        {
            throw new System.NotImplementedException();
        }

        protected override void SetFlyingBool()
        {
            throw new System.NotImplementedException();
        }

        protected override void SetIsRunningBool()
        {
            throw new System.NotImplementedException();
        }
    }
}