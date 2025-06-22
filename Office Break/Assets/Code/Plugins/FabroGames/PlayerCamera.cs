using UnityEngine;

namespace FabroGames.PlayerControlls
{
    public class PlayerCamera : MonoBehaviour
    {
        private const string MOUSE_SENSIVITY_KEY = "mouseSensivity";

        [SerializeField][Range(0f, 120f)] private float _maxYAngle;
        [SerializeField] private Transform _cameraAnchor;

        private PlayerInputActions _playerInputActions;

        private Vector2 _currentRotation;

        #region MONO

        private void OnEnable()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.Disable();

            Cursor.lockState = CursorLockMode.None;
        }

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

            float mouseX = mouseInput.x * PlayerPrefs.GetFloat(MOUSE_SENSIVITY_KEY) * Time.deltaTime;
            float mouseY = mouseInput.y * PlayerPrefs.GetFloat(MOUSE_SENSIVITY_KEY) * Time.deltaTime;

            _currentRotation.x += mouseX;
            _currentRotation.y -= mouseY;

            _currentRotation.y = Mathf.Clamp(_currentRotation.y, -_maxYAngle, _maxYAngle);

            transform.localRotation = Quaternion.Euler(_currentRotation.y, _currentRotation.x, 0f);
        }
    }
}