using System;
using System.Collections.Generic;
using System.Linq;
using OfficeBreak.Core;
using OfficeBreak.Spawners;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies
{
    public class EnemiesController : MonoBehaviour
    {
        [SerializeField] private EnemySpawnController _enemySpawnController;

        private List<Enemy> _activeEnemies = new List<Enemy>();
        private List<Enemy> _attackingEnemies = new List<Enemy>();
        private List<Enemy> _followingEnemies = new List<Enemy>();

        #region MONO

        private void OnEnable() => _enemySpawnController.EnemyWaveSpawned.AddListener(OnEnemyWaveSpawn);

        private void OnDisable() => _enemySpawnController.EnemyWaveSpawned.RemoveListener(OnEnemyWaveSpawn);

        private void Update()
        {
            
        }

        #endregion

        #region CALLBACKS

        private void OnEnemyWaveSpawn(List<Enemy> spawnedEnemies)
        {
            spawnedEnemies
                .ForEach(enemy =>
                {
                    enemy.Health.Died += OnEnemyDeath;
                    _activeEnemies.Add(enemy);
                });
        }

        private void OnEnemyDeath()
        {
            foreach (Enemy enemy in _activeEnemies.Where(enemy => enemy.Health.IsDead))
                _activeEnemies.Remove(enemy);
        }

        #endregion
    }
}