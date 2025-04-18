using OfficeBreak.Core;
using OfficeBreak.Enemies;
using UnityEngine;

namespace OfficeBreak.Spawners
{
    public class EnemySpawner : Spawner
    {
        private Transform _playerTransform;

        private void Start()
        {
            _playerTransform = FindAnyObjectByType<LevelEntryPoint>().PlayerTransform;
            Spawn();
        }

        public override void Spawn()
        {
            GameObject obj = Instantiate(Prefab, transform.position, Quaternion.identity);
            obj.GetComponent<EnemyMover>().Initialize(_playerTransform);
        }
    }
}