using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies.AI
{
    [RequireComponent(typeof(EnemyMover))]
    [RequireComponent(typeof(EnemyAttackController))]
    [DisallowMultipleComponent]
    public class EnemyBehaviourController : MonoBehaviour
    {
        private Transform _playerTransform;
        private EnemyMover _enemyMover;
        private EnemyAttackController _attackController;

        private EnemyBehaviour _followBehaviour;
        private EnemyBehaviour _attackBehaviour;

        public Vector3 PlayerPosition => _playerTransform.position;

        public EnemyAttackController AttackController => _attackController;
        public EnemyBehaviour CurrentBehaviour { get; private set; }

        public void Initialize(Player player)
        {
            _playerTransform = player.transform;
            _enemyMover = GetComponent<EnemyMover>();
            _attackController = GetComponent<EnemyAttackController>();

            _followBehaviour = new EnemyFollowBehaviour(this, _enemyMover);
            _attackBehaviour = new EnemyAttackBehaviour(this, _enemyMover);

            SetFollowBehaviour();
        }

        public void Update() => CurrentBehaviour.Execute();

        public void SetFollowBehaviour() => CurrentBehaviour = _followBehaviour;

        public void SetAttackBehaviour() => CurrentBehaviour = _attackBehaviour;
    }
}