using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies.AI
{
    [RequireComponent(typeof(EnemyMover))]
    [RequireComponent(typeof(EnemyAttackController))]
    public class EnemyBehaviourController : MonoBehaviour
    {
        private Transform _playerTransform;
        private EnemyMover _enemyMover;
        private EnemyAttackController _attackController;

        private EnemyBehaviour _followBehaviour;
        private EnemyBehaviour _attackBehaviour;

        public EnemyBehaviour CurrentBehaviour { get; private set; }

        public void Initialize(Player player)
        {
            _playerTransform = player.transform;
            _enemyMover = GetComponent<EnemyMover>();
            _attackController = GetComponent<EnemyAttackController>();
        }

        private void Start()
        {
            _followBehaviour = new EnemyFollowBehaviour(_playerTransform, _enemyMover);
            _attackBehaviour = new EnemyAttackBehaviour(_playerTransform, _enemyMover, _attackController);

            SetFollowBehaviour();
        }

        public void Update() => CurrentBehaviour.Execute();

        public void SetFollowBehaviour() => CurrentBehaviour = _followBehaviour;

        public void SetAttackBehaviour() => CurrentBehaviour = _attackBehaviour;
    }
}