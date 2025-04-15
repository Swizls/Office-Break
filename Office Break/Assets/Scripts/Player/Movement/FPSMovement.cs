using FabroGames.Input;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FabroGames.Player.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class FPSMovement : MonoBehaviour
    {
        private const float DISTANCE_TO_DETECT_SURFACE_FOR_WALL_JUMP = 2f;
        private const float SPEED_TO_STOP_SLIDE = 8f;

        [SerializeField] private Camera _camera;

        [Header("Movement")]
        [SerializeField] private float _walkingSpeed;
        [SerializeField] private float _spritingMultiplier;
        [SerializeField][Range(0f, 1f)] private float _movementInertia;
        [Space]
        [SerializeField] private float _maxStamina;
        [SerializeField] private float _staminaConsumptionSpeed;
        [SerializeField] private float _staminaRestoreSpeed;
        [Space]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _wallJumpForce;
        [SerializeField][Range(0f, 1f)] private float _jumpInertia;

        private CharacterController _characterController;
        private PlayerInputActions _playerInputActions;
        private Mover _currentMover;

        public Action Jumped;
        public Action<bool> SlideStateChanged;
        public Action StartedStaminaConsumption;
        public Action EndedStaminaConsumtion;

        public Vector2 InputDirection => _playerInputActions.Player.Move.ReadValue<Vector2>();
        public Vector3 Velocity => _characterController.velocity;
        public CharacterController CharacterController => _characterController;
        public Mover CurrentMover => _currentMover;
        public float WalkingSpeed => _walkingSpeed;
        public float SprintingMultiplier => _spritingMultiplier;
        public float MovementInertia => _movementInertia;
        public float JumpInertia => _jumpInertia;
        public bool IsSliding { get; private set; }
        public bool IsRunning => _playerInputActions.Player.Sprint.ReadValue<float>() > 0 && IsMoving;
        public bool IsMoving => Velocity.magnitude > 0;
        public bool IsFlying => !IsGroundend;
        public bool IsGroundend => _characterController.isGrounded;

        #region MONO
        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _currentMover = new WalkMover(this, _playerInputActions, _camera.transform);
        }

        private void OnEnable()
        {
            _playerInputActions = new PlayerInputActions();

            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Jump.performed += Jump;
            _playerInputActions.Player.Sprint.performed += StartRun;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Jump.performed -= Jump;
            _playerInputActions.Player.Sprint.performed -= StartRun;
            _playerInputActions.Player.Disable();
        }

        private void Update()
        {
            _currentMover.ListenMovementInput();
            ListenSlideInput();
        }

        private void FixedUpdate()
        {
            if (Velocity.magnitude > 0 || _currentMover.HasInput || IsFlying)
                _currentMover.Move();
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, Velocity);
        }
        #endregion

        private void ListenSlideInput()
        {
            if (_playerInputActions.Player.Move.ReadValue<Vector2>().y <= 0)
                return;

            if (IsSliding)
                return;

            if (Velocity.magnitude < _walkingSpeed)
                return;

            if (IsRunning == false || _playerInputActions.Player.Crouch.IsPressed() == false)
                return;

            _currentMover = new SlideMover(this, _playerInputActions, _camera.transform);
            StartCoroutine(WaitForSlideEnd());
        }

        private void StartRun(InputAction.CallbackContext context)
        {
            if (IsFlying)
                return;

            if (IsSliding)
                return;

            _currentMover = new RunMover(this, _playerInputActions, _camera.transform);
            StartCoroutine(WaitForRunEnd());
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (IsFlying && CanDoWallJump() == false)
                return;

            Vector3 jumpDirection = Vector3.up;

            if (!IsGroundend)
            {
                if (IsSurfaceOnGivenDirection(-transform.right))
                    jumpDirection += transform.right * _wallJumpForce * Time.deltaTime;
                else if (IsSurfaceOnGivenDirection(transform.right))
                    jumpDirection += -transform.right * _wallJumpForce * Time.deltaTime;
            }

            float jumpStrength = Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y);

            Vector3 velocityY = Vector3.zero;
            velocityY += jumpDirection * Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y);
            _characterController.Move(velocityY * Time.deltaTime);

            IsSliding = false;
            _currentMover = new AirMover(this, _playerInputActions, _camera.transform);

            StartCoroutine(WaitForLand());

            Jumped?.Invoke();

            bool CanDoWallJump()
            {
                return IsSurfaceOnGivenDirection(transform.right) || IsSurfaceOnGivenDirection(-transform.right);
            }

            bool IsSurfaceOnGivenDirection(Vector3 direction)
            {
                return Physics.Raycast(transform.position, direction, DISTANCE_TO_DETECT_SURFACE_FOR_WALL_JUMP, LayerMask.GetMask("Default"));
            }
        }

        private IEnumerator WaitForRunEnd()
        {
            yield return new WaitUntil(() => IsRunning == false);
            _currentMover = new WalkMover(this, _playerInputActions, _camera.transform);
        }

        private IEnumerator WaitForLand()
        {
            yield return new WaitUntil(() => IsGroundend);

            if (_playerInputActions.Player.Sprint.IsPressed())
                _currentMover = new RunMover(this, _playerInputActions, _camera.transform);
            else
                _currentMover = new WalkMover(this, _playerInputActions, _camera.transform);
        }

        private IEnumerator WaitForSlideEnd()
        {
            IsSliding = true;
            SlideStateChanged?.Invoke(IsSliding);

            yield return new WaitUntil(() => Velocity.magnitude < SPEED_TO_STOP_SLIDE || IsSliding == false);

            if (IsFlying)
                _currentMover = new AirMover(this, _playerInputActions, _camera.transform);
            else if (_playerInputActions.Player.Sprint.IsPressed())
                _currentMover = new RunMover(this, _playerInputActions, _camera.transform);
            else
                _currentMover = new WalkMover(this, _playerInputActions, _camera.transform);

            IsSliding = false;
            SlideStateChanged?.Invoke(IsSliding);
        }
    }
}