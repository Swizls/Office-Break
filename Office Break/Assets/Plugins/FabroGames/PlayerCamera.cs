using UnityEngine;

namespace FabroGames.PlayerControlls
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField][Range(0f, 120f)] private float _maxYAngle;
        [SerializeField] private float _sensivity;
        [SerializeField] private Transform _cameraAnchor;

        private PlayerInputActions _playerInputActions;

        private Vector2 _currentRotation;

        #region MONO

        private void Awake() => Cursor.lockState = CursorLockMode.Locked;

        private void OnEnable()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
        }

        private void OnDisable() => _playerInputActions.Player.Disable();

        private void Update() => RotateCamera();

        private void LateUpdate()
        {
            if (_cameraAnchor != null) transform.position = _cameraAnchor.position;
        }

        #endregion

        private void RotateCamera()
        {
            if (Cursor.lockState != CursorLockMode.Locked)
                return;

            Vector2 mouseInput = _playerInputActions.Player.Look.ReadValue<Vector2>();

            float mouseSensivity = _sensivity;

            float mouseX = mouseInput.x * mouseSensivity * Time.deltaTime;
            float mouseY = mouseInput.y * mouseSensivity * Time.deltaTime;

            _currentRotation.x += mouseX;
            _currentRotation.y -= mouseY;

            _currentRotation.y = Mathf.Clamp(_currentRotation.y, -_maxYAngle, _maxYAngle);

            transform.localRotation = Quaternion.Euler(_currentRotation.y, _currentRotation.x, 0f);
        }
    }
}