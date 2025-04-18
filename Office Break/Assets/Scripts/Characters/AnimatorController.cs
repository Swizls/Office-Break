using UnityEngine;

namespace FabroGames.Player
{
    [RequireComponent(typeof(Animator))]
    public abstract class AnimatorController : MonoBehaviour
    {
        protected const string IS_FLYING = "IsFlying";
        protected const string IS_RUNNING = "IsRunning";
        protected const string ATTACK = "Attack";
        protected const string ALTERNATIVE_ATTACK = "AltAttack";

        protected const string MOVE_X = "moveX";
        protected const string MOVE_Y = "moveY";

        protected float ANIMATION_CHANGE_SPEED = 0.1f;

        protected Animator _animator;

        #region MONO
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            SetFlyingBool();
            SetIsRunningBool();
        }
        #endregion

        protected abstract void OnAttackPerform();

        protected abstract void OnAlternativeAttackPerform();

        protected abstract void SetIsRunningBool();

        protected abstract void SetFlyingBool();

        protected void SetMovementDirection(Vector2 relativeMovementDirection)
        {
            _animator.SetFloat(MOVE_X, Mathf.MoveTowards(_animator.GetFloat(MOVE_X), relativeMovementDirection.x, ANIMATION_CHANGE_SPEED));
            _animator.SetFloat(MOVE_Y, Mathf.MoveTowards(_animator.GetFloat(MOVE_Y), relativeMovementDirection.y, ANIMATION_CHANGE_SPEED));
        }

        protected Vector2 CalculateRealitveMovementDirection(Vector3 movementDirection, Vector3 lookDirection)
        {
            float dotProduct = Vector3.Dot(movementDirection.normalized, lookDirection);
            float crossProductMagnitude = Vector3.Cross(movementDirection, lookDirection).y;
            float clockwiseAngle = Mathf.Atan2(crossProductMagnitude, dotProduct) * Mathf.Rad2Deg;

            clockwiseAngle = (clockwiseAngle + 360) % 360;

            float radians = clockwiseAngle * Mathf.Deg2Rad;

            float x = Mathf.Sin(radians);
            float y = Mathf.Cos(radians);

            return new Vector2(x, y);
        }
    }
}