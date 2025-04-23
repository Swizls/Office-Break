using UnityEngine;

namespace OfficeBreak.Characters.Enemies.AI
{
    public abstract class EnemyBehaviour
    {
        protected Transform PlayerTransform { get; private set; }
        protected EnemyMover Mover { get; private set; }

        public EnemyBehaviour(Transform playerTransform, EnemyMover mover)
        {
            PlayerTransform = playerTransform;
            Mover = mover;
        }

        public abstract void Execute();
    }
}