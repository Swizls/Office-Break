using OfficeBreak.Characters;
using OfficeBreak.Characters.Enemies;
using OfficeBreak.Characters.Enemies.AI;
using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        public enum EnemySpawnerType
        {
            Start,
            Elevator
        }

        [SerializeField] private Enemy _prefab;
        [SerializeField] private EnemySpawnerType _type;

        private Transform _playerTransform;
        private Player _player;

        public EnemySpawnerType Type => _type;

        public void Initialize(Transform playerTransform, Player player)
        {
            _playerTransform = playerTransform;
            _player = player;
        }

        public Enemy Spawn()
        {
            Enemy enemy = Instantiate(_prefab, transform.position, Quaternion.identity);

            enemy.Initialize(_playerTransform);
            enemy.GetComponent<EnemyAttackController>().Initialize(_player);
            enemy.GetComponent<EnemyBehaviourController>().Initialize(_player);
            return enemy;
        }
    }
}