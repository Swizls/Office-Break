using FabroGames.PlayerControlls;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OfficeBreak.InteractionSystem
{
    public class ItemHolder : MonoBehaviour
    {
        [SerializeField] private Transform _holdingPoint;
        [SerializeField] private float _throwForce;

        private Item _currentHoldingItem;
        private Transform _cameraTransform;

        private PlayerInputActions _inputActions;

        public event Action ItemPickedUp;
        public event Action ItemDropped;

        private void OnEnable()
        {
            _inputActions = new PlayerInputActions();

            _inputActions.Player.Enable();
            _inputActions.Player.Drop.performed += OnDropKeyPress;
        }

        private void OnDisable()
        {
            _inputActions.Player.Disable();
            _inputActions.Player.Drop.performed -= OnDropKeyPress;
        }

        private void Start() => _cameraTransform = Camera.main.transform;

        private void OnDrawGizmos() => Gizmos.DrawSphere(_holdingPoint.position, 0.2f);

        private void OnDropKeyPress(InputAction.CallbackContext context)
        {
            Drop();
        }

        public void Pickup(Item item)
        {
            if (_currentHoldingItem != null)
                return;

            _currentHoldingItem = item;
            _currentHoldingItem.transform.parent = _holdingPoint;
            _currentHoldingItem.Rigidbody.isKinematic = true;
            _currentHoldingItem.Rigidbody.useGravity = false;
            StartCoroutine(MoveItemToHoldingPoint());
            ItemPickedUp?.Invoke();
        }

        public void Drop()
        {
            _currentHoldingItem.Rigidbody.isKinematic = false;
            _currentHoldingItem.Rigidbody.useGravity = true;
            _currentHoldingItem.transform.parent = null;
            _currentHoldingItem = null;
            ItemDropped?.Invoke();
        }

        public void Throw()
        {
            _currentHoldingItem.Rigidbody.isKinematic = false;
            _currentHoldingItem.Rigidbody.useGravity = true;
            _currentHoldingItem.transform.parent = null;

            Vector3 force = _cameraTransform.forward * _throwForce;

            _currentHoldingItem.Rigidbody.AddForce(force, ForceMode.Impulse);

            _currentHoldingItem = null;
        }

        private IEnumerator MoveItemToHoldingPoint()
        {
            while (_currentHoldingItem != null)
            {
                _currentHoldingItem.transform.localPosition = Vector3.MoveTowards(_currentHoldingItem.transform.localPosition, Vector3.zero, 0.05f);
                _currentHoldingItem.transform.localRotation = Quaternion.Lerp(_currentHoldingItem.transform.localRotation, Quaternion.identity, 0.1f);
                yield return null;
            }
        }
    }
}