using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies
{
    [RequireComponent(typeof(EnemyMover))]
    [RequireComponent(typeof(EnemyAttackController))]
    public class Enemy : MonoBehaviour, IHitable
    {
        [SerializeField] private Health _health;

        private RagdollController _ragdollController;
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

            _ragdollController = GetComponentInChildren<RagdollController>();
        }

        private void Update()
        {
            FollowPlayer();
            AttackPlayer();
        }

        private void OnEnable() => _health.Died += OnDeath;

        private void OnDisable() => _health.Died -= OnDeath;

        #endregion

        private void AttackPlayer()
        {
            if (!_attackController.IsAbleToAttack)
                return;

            if (Vector3.Distance(transform.position, _playerTransform.position) < _attackController.AttackRange)
                _attackController.PerformAttack();
        }

        private void FollowPlayer() => _enemyMover.SetDestination(_playerTransform.position);

        private void OnDeath()
        {
            _ragdollController.EnableRagdoll();
            _attackController.enabled = false;
            enabled = false;
        }

        public void TakeHit(HitData hitData) => _health.TakeDamage(hitData.Damage);

        public void Initialize(Transform playerTranform) => _playerTransform = playerTranform;
    }
}