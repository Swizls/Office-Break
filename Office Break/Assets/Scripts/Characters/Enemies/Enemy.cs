using OfficeBreak.Characters.FightingSystem;
using OfficeBreak.Core.DamageSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies
{
    [RequireComponent(typeof(EnemyMover))]
    [RequireComponent(typeof(EnemyAttackController))]
    public class Enemy : MonoBehaviour, IHitable
    {
        [SerializeField] private Health _health;

        private Transform _playerTransform;
        private EnemyMover _enemyMover;
        private EnemyAttackController _attackController;

        public Health Health => _health;

        #region MONO

        private void Awake()
        {
            _health.Initialize();

            _attackController = GetComponent<EnemyAttackController>();
            _enemyMover = GetComponent<EnemyMover>();
        }

        #endregion

        public void AttackPlayer()
        {
            if (!_attackController.IsAbleToAttack)
                return;

            if (Vector3.Distance(transform.position, _playerTransform.position) < _attackController.AttackRange)
                _attackController.PerformAttack();
        }

        public void TakeHit(HitData hitData) => _health.TakeDamage(hitData.Damage);

        public void Initialize(Transform playerTranform) => _playerTransform = playerTranform;
    }
}