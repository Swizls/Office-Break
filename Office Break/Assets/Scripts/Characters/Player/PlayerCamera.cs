using UnityEngine;

namespace FabroGames.Input
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField][Range(0f, 120f)] private float _maxYAngle;
        [SerializeField] private float _sensivity;
        [SerializeField] private Transform _cameraAnchor;

        private PlayerInputActions _playerInputActions;

        private Vector2 _currentRotation;

        private void Start()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();

            if (!TryGetComponent(out Camera camera))
                return;

            camera.enabled = true;
            GetComponent<AudioListener>().enabled = true;

            enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
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

        private void LateUpdate()
        {
            if (_cameraAnchor != null)
                transform.position = _cameraAnchor.position;
        }
    }
}