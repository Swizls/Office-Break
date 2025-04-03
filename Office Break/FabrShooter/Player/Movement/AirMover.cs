using OfficeBreak.Input;
using OfficeBreak.Player.Movement;
using UnityEngine;

namespace FabrShooter.Player.Movement
{
    public class AirMover : Mover
    {
        public AirMover(FPSMovement playerMovement,
            PlayerInputActions playerInputActions,
            Transform cameraTransform) : base(playerMovement, playerInputActions, cameraTransform) { }

        public override void Move()
        {
            float speed = PlayerMovement.WalkingSpeed * PlayerMovement.SprintingMultiplier;

            Vector3 forwardComponent = Vector3.Project(MovementDirection, PlayerMovement.Velocity.normalized);
            Vector3 sideComponent = MovementDirection - forwardComponent;

            forwardComponent *= PlayerMovement.JumpInertia;
            Vector3 CalculatedMovementDirection = forwardComponent + sideComponent;

            CalculatedVelocity.x = Mathf.MoveTowards(PlayerMovement.Velocity.x, CalculatedMovementDirection.x * speed, PlayerMovement.MovementInertia);
            CalculatedVelocity.y = PlayerMovement.Velocity.y;
            CalculatedVelocity.z = Mathf.MoveTowards(PlayerMovement.Velocity.z, CalculatedMovementDirection.z * speed, PlayerMovement.MovementInertia);

            ApplyGravity();
            CharacterController.Move(CalculatedVelocity * Time.deltaTime);
        }
    }
}