using FabroGames.PlayerControlls;
using OfficeBreak.Characters.FightingSystem;
using System;
using UnityEngine;

namespace OfficeBreak.Characters.Animations
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        private const string IS_RUNNING = "IsRunning";
        private const string IS_BLOCKING = "IsBlocking";
        private const string ATTACK = "Attack";
        private const string ALTERNATIVE_ATTACK = "AltAttack";

        private const string MOVE_X = "moveX";
        private const string MOVE_Y = "moveY";

        private float ANIMATION_CHANGE_SPEED = 0.1f;

        private Animator _animator;
        private IMovable _movable;
        private AttackController _attackController;

        public Action AttackAnimationEnded;

        #region MONO
        private void Awake()
        {
            _movable = GetComponentInParent<IMovable>();
            _attackController = GetComponentInParent<AttackController>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _attackController.AttackPerformed += OnAttackPerform;
            _attackController.AlternativeAttackPerformed += OnAlternativeAttackPerform;
            _attackController.BlockStateChanged += OnBlockStateChange;
        }

        private void OnDisable()
        {
            _attackController.AttackPerformed -= OnAttackPerform;
            _attackController.AlternativeAttackPerformed -= OnAlternativeAttackPerform;
            _attackController.BlockStateChanged -= OnBlockStateChange;
        }

        private void Update()
        {
            SetIsRunningBool();
            Vector3 movementVector = _movable.IsMoving ? _movable.Velocity.normalized : Vector3.zero;
            SetMovementDirection(CalculateRealitveMovementDirection(movementVector, transform.forward));
        }

        #endregion

        private void SetIsRunningBool() => _animator.SetBool(IS_RUNNING, _movable.IsRunning);

        private void OnBlockStateChange(bool flag) => _animator.SetBool(IS_BLOCKING, flag);

        private void OnAttackPerform() => _animator.SetTrigger(ATTACK);

        private void OnAlternativeAttackPerform() => _animator.SetTrigger(ALTERNATIVE_ATTACK);

        private void SetMovementDirection(Vector2 relativeMovementDirection)
        {
            _animator.SetFloat(MOVE_X, Mathf.MoveTowards(_animator.GetFloat(MOVE_X), relativeMovementDirection.x, ANIMATION_CHANGE_SPEED));
            _animator.SetFloat(MOVE_Y, Mathf.MoveTowards(_animator.GetFloat(MOVE_Y), relativeMovementDirection.y, ANIMATION_CHANGE_SPEED));
        }

        private Vector2 CalculateRealitveMovementDirection(Vector3 movementDirection, Vector3 lookDirection)
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