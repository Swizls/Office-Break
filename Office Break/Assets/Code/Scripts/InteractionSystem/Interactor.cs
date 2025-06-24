using UnityEngine;

namespace OfficeBreak.InteractionSystem
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private float _interactionDistance = 2f;
        [SerializeField] private LayerMask _interactionMask;

        private Transform _cameraTransform;

        private void Start ()
        {
            _cameraTransform = Camera.main.transform;
        }

        public void Interact()
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
