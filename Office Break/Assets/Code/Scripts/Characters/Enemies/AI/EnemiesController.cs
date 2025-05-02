using System.Collections.Generic;
using System.Linq;
using OfficeBreak.Spawners;
using UnityEngine;

namespace OfficeBreak.Characters.Enemies.AI
{
    public class EnemiesController : MonoBehaviour
    {
        private const int MAX_ATTACKING_ENEMIES_AT_ONCE = 2;

        [SerializeField] private EnemySpawnController _enemySpawnController;

        private List<Enemy> _activeEnemies = new List<Enemy>();

        private bool IsEnoughAttackingEnemies => AttackingEnemiesCount >= MAX_ATTACKING_ENEMIES_AT_ONCE;
        private int RequiredAttackingEnemiesCount => MAX_ATTACKING_ENEMIES_AT_ONCE - AttackingEnemiesCount;

        public int FollowingEnemiesCount => _activeEnemies.Where(enemy => enemy.BehaviourController.CurrentBehaviour.GetType() == typeof(EnemyFollowBehaviour)).Count();
        public int AttackingEnemiesCount => _activeEnemies.Where(enemy => enemy.BehaviourController.CurrentBehaviour.GetType() == typeof(EnemyAttackBehaviour)).Count();

        #region MONO

        private void OnEnable() => _enemySpawnController.EnemyWaveSpawned.AddListener(OnEnemyWaveSpawn);

        private void OnDisable() => _enemySpawnController.EnemyWaveSpawned.RemoveListener(OnEnemyWaveSpawn);

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

            if (!IsEnoughAttackingEnemies)
                AddAttackingEnemies(RequiredAttackingEnemiesCount);
        }

        private void OnEnemyDeath()
        {
            List<Enemy> deadFollowingEnemies = _activeEnemies.Where(enemy => enemy.Health.IsDead).ToList();

            foreach (Enemy enemy in deadFollowingEnemies)
                _activeEnemies.Remove(enemy);

            List<Enemy> deadAttackingEnemies = _activeEnemies.Where(enemy => enemy.Health.IsDead).ToList();

            foreach (Enemy enemy in deadAttackingEnemies)
                _activeEnemies.Remove(enemy);

            if (!IsEnoughAttackingEnemies)
                AddAttackingEnemies(RequiredAttackingEnemiesCount);
        }

        #endregion

        private void AddAttackingEnemies(int countToAdd)
        {
            List<Enemy> followingEnemies = GetEnemiesByBehaviour<EnemyFollowBehaviour>();

            for (int i = 0; i < countToAdd && i < followingEnemies.Count; i++)
            {
                followingEnemies[i].BehaviourController.SetAttackBehaviour();
            }
        }

        private void AddFollowingEnemies(int countToAdd)
        {
            List<Enemy> attackingEnemies = GetEnemiesByBehaviour<EnemyAttackBehaviour>();

            for (int i = 0; i < countToAdd && i < attackingEnemies.Count; i++)
            {
                attackingEnemies[i].BehaviourController.SetFollowBehaviour();
            }
        }

        private List<Enemy> GetEnemiesByBehaviour<T>() where T : EnemyBehaviour
        {
            return _activeEnemies.Where(enemy => enemy.BehaviourController.CurrentBehaviour.GetType() == typeof(T)).ToList();
        }
    }
}