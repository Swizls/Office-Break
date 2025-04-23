using OfficeBreak.Characters.Enemies.AI;
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

        private EnemyAttackController _attackController;

        public Health Health => _health;
        public EnemyBehaviourController BehaviourController { get; private set; }

        #region MONO

        private void Awake()
        {
            _health.Initialize();

            _attackController = GetComponent<EnemyAttackController>();
            BehaviourController = GetComponent<EnemyBehaviourController>();
        }

        #endregion

        public void Initialize(Player player)
        {
            _attackController.Initialize(player);
            BehaviourController.Initialize(player);
        }

        public void TakeHit(HitData hitData) => _health.TakeDamage(hitData.Damage);

    }
}