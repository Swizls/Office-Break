using OfficeBreak.Characters;
using OfficeBreak.Characters.Enemies;
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

        private Player _player;

        public EnemySpawnerType Type => _type;

        public void Initialize(Player player) => _player = player;

        public Enemy Spawn()
        {
            Enemy enemy = Instantiate(_prefab, transform.position, Quaternion.identity);
            enemy.Initialize(_player);
            return enemy;
        }
    }
}