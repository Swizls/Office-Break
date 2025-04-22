using OfficeBreak.Core.DamageSystem;
using UnityEngine;

namespace OfficeBreak.Characters
{
    public abstract class DeathHandler : MonoBehaviour
    {
        protected Health _health;
        protected RagdollController _ragdollController;

        private void OnEnable() => _health.Died += HandleDeath;

        private void OnDisable() => _health.Died -= HandleDeath;

        protected abstract void HandleDeath();
    }
}