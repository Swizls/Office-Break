using UnityEngine;

namespace OfficeBreak.Characters.Enemies
{
    [RequireComponent(typeof(EnemyMover))]
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour, IHitable
    {
        [SerializeField] private RagdollController _ragdollController;

        private Health _health;
        private EnemyMover _enemyMover;

        public float HealthValue => _health.Value;

        #region MONO

        private void Awake()
        {
            _health = GetComponent<Health>();
            _enemyMover = GetComponent<EnemyMover>();
        }

        private void OnEnable() => _health.Died += OnDeath;

        private void OnDisable() => _health.Died -= OnDeath;

        #endregion

        private void OnDeath() => _ragdollController.EnableRagdoll();

        public void TakeHit(HitData hitData) => _health.TakeDamage(hitData.Damage);
    }
}