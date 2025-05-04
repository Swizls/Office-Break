using OfficeBreak.Core;
using OfficeBreak.Spawners;
using UnityEngine;

namespace OfficeBreak.Characters
{
    public class Healer : MonoBehaviour
    {
        private EnemySpawnController _spawnController;
        private Health _targetHealth;

        private void Awake()
        {
            _spawnController = FindAnyObjectByType<EnemySpawnController>();
            _targetHealth = FindAnyObjectByType<Player>().Health;
        }

        private void OnEnable() => _spawnController.EnemyWaveDefeated += OnDeathAllEnemies;

        private void OnDisable() => _spawnController.EnemyWaveDefeated -= OnDeathAllEnemies;

        private void OnDeathAllEnemies() => _targetHealth.TakeHeal(1000);
    }
}