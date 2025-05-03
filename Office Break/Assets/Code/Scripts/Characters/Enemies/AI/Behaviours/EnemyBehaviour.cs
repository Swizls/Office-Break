using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies.AI
{
    public abstract class EnemyBehaviour
    {
        protected const float CIRCLE_RADIUS = 5f;

        protected readonly float ChosenAngle = 0f;

        protected EnemyAttackController AttackController { get; private set; }
        protected EnemyBehaviourController BehaviourController { get; private set; }
        protected Transform EnemyTransform { get; private set; }
        protected EnemyMover Mover { get; private set; }

        public EnemyBehaviour(EnemyBehaviourController controller, EnemyMover mover)
        {
            BehaviourController = controller;
            AttackController = controller.AttackController;
            EnemyTransform = mover.transform;
            Mover = mover;

            ChosenAngle = Random.Range(0f, 360f);
        }

        public abstract void Execute();

        protected Vector3 CalculatePointAroundPlayer(float radius, float angle)
        {
            float x = BehaviourController.PlayerPosition.x + radius * Mathf.Cos(angle);
            float z = BehaviourController.PlayerPosition.z + radius * Mathf.Sin(angle);
            Vector3 calculatedPos = new Vector3(x, BehaviourController.PlayerPosition.y, z);

            return calculatedPos;
        }
    }
}