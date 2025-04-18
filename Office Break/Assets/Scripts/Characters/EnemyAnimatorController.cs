using OfficeBreak.Characters.Enemies;
using UnityEngine;

namespace FabroGames.Characters.Animations
{
    public class EnemyAnimatorController : AnimatorController
    {
        [SerializeField] private EnemyMover _enemyMover;

        private void Update()
        {
            Vector2 movementDirection = CalculateRealitveMovementDirection(_enemyMover.AgentVelocity, _enemyMover.transform.forward);
            SetMovementDirection(movementDirection);
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(_enemyMover.transform.position, _enemyMover.AgentVelocity.normalized, Color.green);
            Debug.DrawRay(_enemyMover.transform.position, _enemyMover.transform.forward, Color.red);
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