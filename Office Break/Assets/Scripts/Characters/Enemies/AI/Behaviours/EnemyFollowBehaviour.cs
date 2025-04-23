using UnityEngine;

namespace OfficeBreak.Characters.Enemies.AI
{
    public class EnemyFollowBehaviour : EnemyBehaviour
    {
        private const float CIRCLE_RADIUS = 5f;

        private readonly float _chosenAngle;

        public EnemyFollowBehaviour(Transform playerTransform, EnemyMover mover) : base(playerTransform, mover)
        {
            _chosenAngle = Random.Range(0, 360);
        }

        public override void Execute()
        {
            Vector3 targetPosition = GetPositionOnCircleAroundPlayer();

            Mover.SetDestination(targetPosition);
        }

        private Vector3 GetPositionOnCircleAroundPlayer(int attempt = 0)
        {
            if (attempt > 10)
                return Mover.transform.position;

            Vector3 position = CalculatePointAroundPlayer();

            //if (Mover.IsPositionReachable(position))
            //    return position;

            return position;
            //return GetPositionOnCircleAroundPlayer(attempt++);
        }

        private Vector3 CalculatePointAroundPlayer()
        {
            float x = PlayerTransform.position.x + CIRCLE_RADIUS * Mathf.Cos(_chosenAngle);
            float z = PlayerTransform.position.z + CIRCLE_RADIUS * Mathf.Sin(_chosenAngle);
            Vector3 calculatedPos = new Vector3(x, PlayerTransform.position.y, z);

            return calculatedPos;
        }
    }

}