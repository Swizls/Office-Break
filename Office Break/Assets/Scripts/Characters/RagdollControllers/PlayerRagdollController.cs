using FabroGames.Input;
using FabroGames.Player.Movement;
using UnityEngine;

namespace OfficeBreak.Characters
{
    public class PlayerRagdollController : RagdollController
    {
        [SerializeField] private PlayerCamera _camera;

        private FPSMovement _movement;

        protected override void Initialize()
        {
            _movement = GetComponentInParent<FPSMovement>();

            base.Initialize();
        }

        public override void EnableRagdoll()
        {
            base.EnableRagdoll();

            _movement.enabled = false;
            _camera.enabled = false;
        }

        public override void DisableRagdoll()
        {
            base.DisableRagdoll();

            _movement.enabled = true;
            _camera.enabled = true;
        }
    }
}