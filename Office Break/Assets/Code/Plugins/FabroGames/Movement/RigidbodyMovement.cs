using UnityEngine;
using UnityEngine.InputSystem;

namespace FabroGames.PlayerControlls
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMovement : MonoBehaviour,  IMovable
    {
        private const float GROUND_DETECTION_DISTANCE = 1.2f;

        [SerializeField] private Transform _cameraTransform;

        [Header("Movement")]
        [SerializeField] private float _walkingSpeed;
        [SerializeField] private float _spritingMultiplier;
        [SerializeField] private float _jumpForce;

        private PlayerInputActions _playerInputActions;
        private Rigidbody _rigidbody;
        private Vector3 _inputDirection;

        public Vector3 Velocity => _rigidbody.linearVelocity;
        public bool IsRunning => _playerInputActions.Player.Sprint.ReadValue<float>() > 0 && IsMoving;
        public bool IsMoving => Velocity.magnitude > 0.01f;
        public bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, GROUND_DETECTION_DISTANCE, LayerMask.GetMask("Default"));

        public bool Enabled { get => enabled; set => enabled = value; }

        #region MONO

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _playerInputActions = new PlayerInputActions();

            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Jump.performed += Jump;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Jump.performed -= Jump;
            _playerInputActions.Player.Disable();
        }

        private void Update()
        {
            ListenMovementInput();
        }

        private void FixedUpdate() => Move();

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, _rigidbody.linearVelocity);
        }

        #endregion

        private void ListenMovementInput()
        {
            Vector2 playerInput = _playerInputActions.Player.Move.ReadValue<Vector2>();

            Vector3 forward = _cameraTransform.TransformDirection(Vector3.forward);
            Vector3 right = _cameraTransform.TransformDirection(Vector3.right);

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            _inputDirection = forward * playerInput.y + right * playerInput.x;
        }

        private void Move()
        {
            //if (_inputDirection == Vector3.zero)
            //{
            //    float yVelocty = IsGrounded ? 0 : _rigidbody.linearVelocity.y;
            //    _rigidbody.linearVelocity = new Vector3(0, yVelocty, 0);
            //    return;
            //}

            float speed = IsRunning ? _walkingSpeed * _spritingMultiplier : _walkingSpeed;
            Vector3 movementForce = _inputDirection * speed * Time.deltaTime;
            movementForce.y = _rigidbody.linearVelocity.y;
            movementForce = AdjustVelocityToSlope(movementForce);
            _rigidbody.linearVelocity = movementForce;
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (!IsGrounded)
                return;

            Vector3 jumpForce = Vector3.up * _jumpForce;
            _rigidbody.AddForce(jumpForce, ForceMode.Impulse);
        }

        private Vector3 AdjustVelocityToSlope(Vector3 velocity)
        {
            Ray ray = new Ray(transform.position, Vector3.down);

            if (!Physics.Raycast(ray, out RaycastHit hitInfo, GROUND_DETECTION_DISTANCE, LayerMask.GetMask("Default")))
                return velocity;

            Vector3 projectedVector = Vector3.ProjectOnPlane(velocity, hitInfo.normal);

            return projectedVector;
        }
    }
}
