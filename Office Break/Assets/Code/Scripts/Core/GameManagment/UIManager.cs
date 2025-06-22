using FabroGames.PlayerControlls;
using OfficeBreak.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace OfficeBreak.Core.GameManagment
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _exitButton;

        private PlayerCamera _playerCamera;
        private RigidbodyMovement _playerMovement;

        private PlayerInputActions _inputActions;

        private void Start()
        {
            _pauseMenu.SetActive(false);
        }

        private void OnEnable()
        {
            if (_inputActions == null)
                _inputActions = new PlayerInputActions();

            _inputActions.UI.Enable();
            _inputActions.UI.Pause.performed += TogglePauseMenu;

            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable() => _inputActions.UI.Disable();

        public void Initialize(PlayerCamera playerCamera, RigidbodyMovement playerMovement)
        {
            _playerCamera = playerCamera;
            _playerMovement = playerMovement;
        }

        private void TogglePauseMenu(InputAction.CallbackContext context)
        {
            bool value = !_pauseMenu.activeInHierarchy;

            _pauseMenu.SetActive(value);
            _playerCamera.enabled = !value;
            _playerMovement.enabled = !value;
        }

        private void OnMainMenuButtonClick() => SceneLoader.LoadMainMenu();

        private void OnExitButtonClick() => Application.Quit();
    }
}