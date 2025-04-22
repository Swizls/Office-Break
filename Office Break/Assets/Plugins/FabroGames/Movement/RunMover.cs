using UnityEngine;

namespace FabroGames.PlayerControlls
{
    public class RunMover : Mover
    {
        public RunMover(FPSMovement playerMovement, PlayerInputActions playerInputActions, Transform cameraTransform) : base(playerMovement, playerInputActions, cameraTransform) { }

        public override void Move()
        {
            CalculatedVelocity = Vector3.MoveTowards(PlayerMovement.Velocity, MovementDirection * PlayerMovement.WalkingSpeed * PlayerMovement.SprintingMultiplier, PlayerMovement.MovementInertia);

            CalculatedVelocity.y = PlayerMovement.Velocity.y;

            CalculatedVelocity = AdjustVelocityToSlope(CalculatedVelocity);

            ApplyGravity();
            CharacterController.Move(CalculatedVelocity * Time.deltaTime);
        }
    }
}