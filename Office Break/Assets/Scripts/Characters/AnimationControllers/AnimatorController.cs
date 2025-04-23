using UnityEngine;

namespace OfficeBreak.Characters.Animations
{
    [RequireComponent(typeof(Animator))]
    public abstract class AnimatorController : MonoBehaviour
    {
        protected const string IS_FLYING = "IsFlying";
        protected const string IS_RUNNING = "IsRunning";
        protected const string IS_BLOCKING = "IsBlocking";
        protected const string ATTACK = "Attack";
        protected const string ALTERNATIVE_ATTACK = "AltAttack";

        protected const string MOVE_X = "moveX";
        protected const string MOVE_Y = "moveY";

        protected float ANIMATION_CHANGE_SPEED = 0.1f;

        [SerializeField] private AnimationClip _primaryAttackAnimation;
        [SerializeField] private AnimationClip _secondaryAttackAnimation;

        protected Animator _animator;

        public float PrimaryAttackAnimationLength => _primaryAttackAnimation.length;
        public float SecondaryAttackAnimationLength => _secondaryAttackAnimation.length;

        #region MONO
        private void Start() => _animator = GetComponent<Animator>();
        #endregion

        protected abstract void SetIsRunningBool();

        protected abstract void SetFlyingBool();

        protected void OnBlockStateChange(bool flag) => _animator.SetBool(IS_BLOCKING, flag);

        protected void OnAttackPerform() => _animator.SetTrigger(ATTACK);

        protected void OnAlternativeAttackPerform() => _animator.SetTrigger(ALTERNATIVE_ATTACK);

        protected void SetMovementDirection(Vector2 relativeMovementDirection)
        {
            _animator.SetFloat(MOVE_X, Mathf.MoveTowards(_animator.GetFloat(MOVE_X), relativeMovementDirection.x, ANIMATION_CHANGE_SPEED));
            _animator.SetFloat(MOVE_Y, Mathf.MoveTowards(_animator.GetFloat(MOVE_Y), relativeMovementDirection.y, ANIMATION_CHANGE_SPEED));
        }

        protected Vector2 CalculateRealitveMovementDirection(Vector3 movementDirection, Vector3 lookDirection)
        {
            if (movementDirection.magnitude == 0)
                return Vector2.zero;

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