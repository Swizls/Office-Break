using OfficeBreak.Core;
using UnityEngine;

namespace OfficeBreak.Characters
{
    public abstract class DeathHandler : MonoBehaviour
    {
        private const int KNOCKOUT_FORCE = 100;

        protected Health Health;
        protected RagdollController _ragdollController;

        private void OnEnable() => Health.Died += HandleDeath;

        private void OnDisable() => Health.Died -= HandleDeath;

        protected virtual void HandleDeath()
        {
            _ragdollController.EnableRagdoll();
            _ragdollController.ApplyForce(-_ragdollController.transform.forward * KNOCKOUT_FORCE);
        }
    }
}