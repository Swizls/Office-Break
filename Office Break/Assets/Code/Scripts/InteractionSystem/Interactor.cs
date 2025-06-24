using FabroGames.PlayerControlls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OfficeBreak.InteractionSystem
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private float _interactionDistance = 2f;
        [SerializeField] private LayerMask _interactionMask;

        private Transform _cameraTransform;
        private PlayerInputActions _inputActions;

        private void Start ()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void OnEnable()
        {    
            _inputActions = new PlayerInputActions();

            _inputActions.Player.Enable();
            _inputActions.Player.Interact.performed += Interact;
        }

        private void OnDisable()
        {
            _inputActions.Player.Interact.performed -= Interact;
            _inputActions.Player.Disable();
        }

        private void Interact(InputAction.CallbackContext context)
        {
            Debug.Log("Interacting");
            Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, _interactionDistance);

            if (hit.collider == null)
                return;

            if (!hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
                return;

            interactable.Interact(this).Execute();
        }
    }
}
