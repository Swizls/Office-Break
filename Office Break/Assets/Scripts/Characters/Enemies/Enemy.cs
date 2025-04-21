using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies
{
    [RequireComponent(typeof(EnemyMover))]
    [RequireComponent(typeof(EnemyAttackController))]
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour, IHitable
    {
        [SerializeField] private RagdollController _ragdollController;

        private Transform _playerTransform;
        private Health _health;
        private EnemyAttackController _attackController;

        public Health Health => _health;

        #region MONO

        private void Awake()
        {
            _health = GetComponent<Health>();
            _attackController = GetComponent<EnemyAttackController>();
        }

        private void Update()
        {
            if (!_attackController.IsAbleToAttack)
                return;

            if (Vector3.Distance(transform.position, _playerTransform.position) < _attackController.AttackRange)
                _attackController.PerformAttack();
        }

        private void OnEnable() => _health.Died += OnDeath;

        private void OnDisable() => _health.Died -= OnDeath;

        #endregion

        private void OnDeath()
        {
            _ragdollController.EnableRagdoll();
            _attackController.enabled = false;
            enabled = false;
        }

        public void TakeHit(HitData hitData) => _health.TakeDamage(hitData.Damage);

        public void Initialize(Transform playerTranform)
        {
            _playerTransform = playerTranform;
        }
    }
}