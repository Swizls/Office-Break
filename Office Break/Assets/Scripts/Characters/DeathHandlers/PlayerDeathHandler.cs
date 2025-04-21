using FabroGames.Player.Movement;
using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters
{
    [RequireComponent(typeof(FPSMovement))]
    [RequireComponent(typeof(PlayerAttackController))]
    public class PlayerDeathHandler : DeathHandler
    {
        private void Awake()
        {
            _health = GetComponent<Player>().Health;

            _ragdollController = GetComponentInChildren<RagdollController>();
        }

        protected override void HandleDeath() => _ragdollController.EnableRagdoll();
    }
}