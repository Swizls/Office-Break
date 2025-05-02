using OfficeBreak.Characters.Enemies;
using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters
{
    [RequireComponent(typeof(EnemyAttackController))]
    [RequireComponent(typeof(EnemyMover))]
    public class EnemyDeathHandler : DeathHandler
    {
        private void Awake()
        {
            Health = GetComponent<Enemy>().Health;

            _ragdollController = GetComponentInChildren<RagdollController>();
        }
    }
}