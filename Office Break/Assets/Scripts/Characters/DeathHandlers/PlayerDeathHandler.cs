using FabroGames.Characters.Player;
using FabroGames.Player.Movement;
using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters
{
    [RequireComponent(typeof(FPSMovement))]
    [RequireComponent(typeof(PlayerAttackController))]
    public class PlayerDeathHandler : DeathHandler
    {
        [SerializeField] private DeathCamera _deathCamera;
        [SerializeField] private CopyCameraRotation _cameraRotationCopier;

        private void Awake()
        {
            _health = GetComponent<Player>().Health;

            _ragdollController = GetComponentInChildren<RagdollController>();
        }

        protected override void HandleDeath()
        {
            _ragdollController.EnableRagdoll();

            _cameraRotationCopier.enabled = false;

            _deathCamera.enabled = true;
        }
    }
}