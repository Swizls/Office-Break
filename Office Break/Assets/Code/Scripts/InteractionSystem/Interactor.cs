using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

namespace OfficeBreak.InteractionSystem
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private float _interactionDistance = 2f;
        [SerializeField] private LayerMask _interactionMask;
        [SerializeField] private Material _outlineMaterial;

        private MeshRenderer _currentOutlinedMesh;
        private List<Material> _meshOriginalMaterials = new List<Material>();

        private Transform _cameraTransform;

        private void Start() => _cameraTransform = Camera.main.transform;

        private void Update() => HighlighteInteractable();

        public void Interact()
        {
            Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, _interactionDistance);

            if (hit.collider == null)
                return;

            if (!hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
                return;

            interactable.Interact(this).Execute();
        }

        private void HighlighteInteractable()
        {
            Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, _interactionDistance);

            if(hit.collider == null || !IsInteractable())
            {
                if (_currentOutlinedMesh == null)
                    return;

                _currentOutlinedMesh.SetMaterials(_meshOriginalMaterials);
                _currentOutlinedMesh = null;
                return;
            }

            MeshRenderer newMeshRenderer = hit.collider.gameObject.GetComponentInParent<MeshRenderer>();

            if (newMeshRenderer == null)
                return;

            if (_currentOutlinedMesh == newMeshRenderer)
                return;

            if(_currentOutlinedMesh != null)
                _currentOutlinedMesh.SetMaterials(_meshOriginalMaterials);

            _meshOriginalMaterials = newMeshRenderer.materials.ToList();
            _currentOutlinedMesh = newMeshRenderer;

            List<Material> materials = new List<Material>();

            foreach (Material material in _currentOutlinedMesh.materials)
                materials.Add(material);

            materials.Add(_outlineMaterial);

            _currentOutlinedMesh.SetMaterials(materials);

            bool IsInteractable()
            {
                return hit.collider.gameObject.GetComponentInParent<IInteractable>() != null;
            }
        }
    }
}