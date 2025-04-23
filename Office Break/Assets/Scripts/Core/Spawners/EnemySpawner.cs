using OfficeBreak.Characters;
using OfficeBreak.Characters.Enemies;
using OfficeBreak.Characters.FightingSystem;
using OfficeBreak.Core;
using OfficeBreak.Core.DamageSystem;
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

        private Transform _playerTransform;
        private Player _player;

        public EnemySpawnerType Type => _type;
        public Enemy LastSpawnedEnemy { get; private set; }

        public void Initialize(Transform playerTransform, Player player)
        {
            _playerTransform = playerTransform;
            _player = player;
        }

        public override void Spawn()
        {
            GameObject obj = Instantiate(Prefab, transform.position, Quaternion.identity);

            var enemyComponent = obj.GetComponent<Enemy>();
            enemyComponent.Initialize(_playerTransform);
            LastSpawnedEnemy = enemyComponent;

            obj.GetComponent<EnemyAttackController>().Initialize(_player);
        }
    }
}