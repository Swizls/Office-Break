using System;
using System.Collections;
using UnityEngine;

namespace OfficeBreak.InteractionSystem
{
    public class ItemHolder : MonoBehaviour
    {
        private const float ITEM_PICKUP_SPEED = 0.1f;
        [SerializeField] private Transform _holdingPoint;
        [SerializeField] private float _throwForce;

        private Item _currentHoldingItem;
        private Transform _cameraTransform;

        public event Action ItemPickedUp;
        public event Action ItemDropped;

        public bool IsCarringItem => _currentHoldingItem != null;

        #region MONO

        private void Start() => _cameraTransform = Camera.main.transform;

        private void OnDrawGizmos() => Gizmos.DrawSphere(_holdingPoint.position, 0.2f);

        private void OnDisable()
        {
            if (_currentHoldingItem != null)
                Drop();
        }

        #endregion MONO

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
            _currentHoldingItem.Throw(force);

            _currentHoldingItem = null;

            ItemDropped?.Invoke();
        }

        private IEnumerator MoveItemToHoldingPoint()
        {
            while (_currentHoldingItem != null)
            {
                _currentHoldingItem.transform.localPosition = Vector3.MoveTowards(_currentHoldingItem.transform.localPosition, Vector3.zero, ITEM_PICKUP_SPEED);
                _currentHoldingItem.transform.localRotation = Quaternion.Lerp(_currentHoldingItem.transform.localRotation, Quaternion.identity, 0.1f);
                yield return null;
            }
        }
    }
}