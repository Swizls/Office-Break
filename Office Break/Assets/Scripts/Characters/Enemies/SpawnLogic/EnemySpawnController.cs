using OfficeBreak.Characters;
using OfficeBreak.Characters.Enemies;
using OfficeBreak.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace OfficeBreak.Spawners
{
    public class EnemySpawnController : MonoBehaviour
    {
        [SerializeField] private float _enemySpawnDelay = 5f;

        private List<EnemySpawner> _elevatorSpawners;
        private List<EnemySpawner> _startEnemySpawners;

        private int _activeEnemyCount;

        public UnityEvent<List<Enemy>> EnemyWaveSpawned;

        #region MONO

        private void Awake()
        {
            List<EnemySpawner> enemySpawners = FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None).ToList();

            _elevatorSpawners = enemySpawners.Where(spawner => spawner.Type == EnemySpawner.EnemySpawnerType.Elevator).ToList();
            _startEnemySpawners = enemySpawners.Where(spawner => spawner.Type == EnemySpawner.EnemySpawnerType.Start).ToList();

            Transform playerTransform = FindAnyObjectByType<LevelEntryPoint>().PlayerTransform;
            Player player = playerTransform.GetComponent<Player>();

            foreach (var spawner in enemySpawners)
                spawner.Initialize(playerTransform, player);
        }

        private void Start() => Spawn(_startEnemySpawners);

        private void Spawn(List<EnemySpawner> spawners)
        {
            List<Enemy> enemies = new List<Enemy>();
            foreach (var spawner in spawners)
            {
                Enemy enemy = spawner.Spawn();
                enemy.Health.Died += OnEnemyDeath;
                enemies.Add(enemy);
                _activeEnemyCount++;
            }

            EnemyWaveSpawned?.Invoke(enemies);
        }

        #endregion

        private void OnEnemyDeath()
        {
            _activeEnemyCount--;

            if (_activeEnemyCount == 0)
                StartCoroutine(SpawnEnemiesWithDelay(_enemySpawnDelay));
        }

        private IEnumerator SpawnEnemiesWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Spawn(_elevatorSpawners);
        }
    }
}