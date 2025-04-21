using OfficeBreak.Characters.Enemies;
using OfficeBreak.Characters.FightingSystem;
using OfficeBreak.Core;
using OfficeBreak.Enemies;
using UnityEngine;

namespace OfficeBreak.Spawners
{
    public class EnemySpawner : Spawner
    {
        public enum EnemySpawnerType
        {
            Start,
            Elevator
        }

        [SerializeField] private EnemySpawnerType _type;
        [SerializeField] private Transform[] points;

        private Transform _playerTransform;
        private Health _playerHealth;

        public EnemySpawnerType Type => _type;
        public Enemy LastSpawnedEnemy { get; private set; }

        public void Initialize(Transform playerTransform, Health playerHealth)
        {
            _playerTransform = FindAnyObjectByType<LevelEntryPoint>().PlayerTransform;
            _playerHealth = _playerTransform.GetComponent<Health>();
        }

        public override void Spawn()
        {
            GameObject obj = Instantiate(Prefab, transform.position, Quaternion.identity);

            var enemyComponent = obj.GetComponent<Enemy>();
            enemyComponent.Initialize(_playerTransform);
            LastSpawnedEnemy = enemyComponent;

            obj.GetComponent<EnemyMoverTester>().Initialize(_playerTransform, points);
            obj.GetComponent<EnemyAttackController>().Initialize(_playerHealth);
        }
    }
}