using FabroGames.Characters.Player;
using OfficeBreak.Characters.FightingSystem;
using OfficeBreak.Core;
using UnityEngine;

namespace OfficeBreak.Characters
{
    [RequireComponent(typeof(PlayerAttackController))]
    public class PlayerDeathHandler : DeathHandler
    {
        [SerializeField] private DeathCamera _deathCamera;
        [SerializeField] private CopyCameraRotation _cameraRotationCopier;

        private void Awake()
        {
            Health = GetComponent<Player>().Health;

            _ragdollController = GetComponentInChildren<RagdollController>();
        }

        protected override void HandleDeath()
        {
            base.HandleDeath();

            _cameraRotationCopier.enabled = false;

            _deathCamera.enabled = true;
        }
    }
}