using OfficeBreak.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace OfficeBreak.Spawners
{
    public class EnemySpawnController : MonoBehaviour
    {
        [SerializeField] private List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();
        [SerializeField] private UnityEvent EnemyWaveSpawned;

        private List<EnemySpawner> _elevatorSpawners;
        private List<EnemySpawner> _startEnemySpawners;

        private int _activeEnemyCount;

        #region MONO

        private void Awake()
        {
            _elevatorSpawners = _enemySpawners.Where(spawner => spawner.Type == EnemySpawner.EnemySpawnerType.Elevator).ToList();
            _startEnemySpawners = _enemySpawners.Where(spawner => spawner.Type == EnemySpawner.EnemySpawnerType.Start).ToList();

            Transform playerTransform = FindAnyObjectByType<LevelEntryPoint>().PlayerTransform;
            Health playerHealth = playerTransform.GetComponent<Health>();

            foreach (var spawner in _enemySpawners)
                spawner.Initialize(playerTransform, playerHealth);
        }

        private void Start() => SpawnEnemies(_startEnemySpawners);

        private void SpawnEnemies(List<EnemySpawner> spawners)
        {
            foreach (var spawner in spawners)
            {
                spawner.Spawn();
                _activeEnemyCount++;
                spawner.LastSpawnedEnemy.Health.Died += OnEnemyDeath;
            }

            EnemyWaveSpawned?.Invoke();
        }

        #endregion

        private void OnEnemyDeath()
        {
            _activeEnemyCount--;

            if (_activeEnemyCount == 0)
                SpawnEnemies(_elevatorSpawners);
        }
    }
}