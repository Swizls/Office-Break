using OfficeBreak.Characters.Enemies;
using OfficeBreak.Characters.FightingSystem;
using OfficeBreak.Core;
using OfficeBreak.Enemies;
using UnityEngine;

namespace OfficeBreak.Spawners
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] private Transform[] points;

        private Transform _playerTransform;
        private Health _playerHealth;

        private void Start()
        {
            _playerTransform = FindAnyObjectByType<LevelEntryPoint>().PlayerTransform;
            _playerHealth = _playerTransform.GetComponent<Health>();
            Spawn();
        }

        public override void Spawn()
        {
            GameObject obj = Instantiate(Prefab, transform.position, Quaternion.identity);
            obj.GetComponent<EnemyMoverTester>().Initialize(_playerTransform, points);
            obj.GetComponent<Enemy>().Initialize(_playerTransform);
            obj.GetComponent<EnemyAttackController>().Initialize(_playerHealth);
        }
    }
}