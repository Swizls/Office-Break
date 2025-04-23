using System;
using System.Collections.Generic;
using System.Linq;
using OfficeBreak.Spawners;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace OfficeBreak.Characters.Enemies
{
    public class EnemiesController : MonoBehaviour
    {
        private const int MAX_ATTACKING_AT_ONCE_ENEMIES = 2;

        [SerializeField] private EnemySpawnController _enemySpawnController;

        private List<Enemy> _attackingEnemies = new List<Enemy>();
        private List<Enemy> _followingEnemies = new List<Enemy>();

        private bool IsEnoughAttackingEnemies => _attackingEnemies.Count >= MAX_ATTACKING_AT_ONCE_ENEMIES;
        private int RequiredAttackingEnemiesCount => MAX_ATTACKING_AT_ONCE_ENEMIES - _attackingEnemies.Count;

        public int FollowingEnemiesCount => _followingEnemies.Count;
        public int AttackingEnemiesCount => _attackingEnemies.Count;

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
                    _followingEnemies.Add(enemy);
                });

            if (!IsEnoughAttackingEnemies)
                AddAttackingEnemies(RequiredAttackingEnemiesCount);
        }

        private void OnEnemyDeath()
        {
            List<Enemy> deadFollowingEnemies = _followingEnemies.Where(enemy => enemy.Health.IsDead).ToList();

            foreach (Enemy enemy in deadFollowingEnemies)
                _followingEnemies.Remove(enemy);

            List<Enemy> deadAttackingEnemies = _attackingEnemies.Where(enemy => enemy.Health.IsDead).ToList();

            foreach (Enemy enemy in deadAttackingEnemies)
                _attackingEnemies.Remove(enemy);

            if (!IsEnoughAttackingEnemies)
                AddAttackingEnemies(RequiredAttackingEnemiesCount);
        }

        #endregion

        private void AddAttackingEnemies(int countToAdd)
        {
            TransferEnemies(_followingEnemies, _attackingEnemies, countToAdd);

            _attackingEnemies.ForEach(enemy => enemy.BehaviourController.SetAttackBehaviour());
        }

        private void TransferEnemies(List<Enemy> fromList, List<Enemy> toList, int count)
        {
            List<Enemy> transferedEnemies = new List<Enemy>();

            for(int i = 0; i < count && i < fromList.Count; i++) 
            {
                transferedEnemies.Add(fromList[i]);
                toList.Add(fromList[i]);
            }

            fromList.RemoveAll(enemy => transferedEnemies.Contains(enemy));
        }
    }
}