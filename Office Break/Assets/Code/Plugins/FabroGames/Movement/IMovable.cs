using UnityEngine;

namespace FabroGames.PlayerControlls
{
    public interface IMovable 
    { 
        public Vector3 Velocity { get; }
        public bool IsRunning { get; }
        public bool IsMoving { get; }
        public bool IsGrounded { get; }
        public bool Enabled { get; set; }
    }
}
