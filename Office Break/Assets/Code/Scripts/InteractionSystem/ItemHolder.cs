using System.Collections;
using UnityEngine;

namespace OfficeBreak.InteractionSystem
{
    public class ItemHolder : MonoBehaviour
    {
        [SerializeField] private Transform _holdingPoint;
        [SerializeField] private float _throwForce;
        [SerializeField] private ForceMode _forceMode;

        private Item _currentHoldingItem;
        private Transform _cameraTransform;

        private void Start() => _cameraTransform = Camera.main.transform;

        private void OnDrawGizmos() => Gizmos.DrawSphere(_holdingPoint.position, 0.2f);

        public void Pickup(Item item)
        {
            if (_currentHoldingItem != null)
                return;

            _currentHoldingItem = item;
            _currentHoldingItem.transform.parent = _holdingPoint;
            _currentHoldingItem.Rigidbody.isKinematic = true;
            _currentHoldingItem.Rigidbody.useGravity = false;
            StartCoroutine(MoveItemToHoldingPoint());
        }

        public void Drop()
        {
            _currentHoldingItem.Rigidbody.isKinematic = false;
            _currentHoldingItem.Rigidbody.useGravity = true;
            _currentHoldingItem = null;
        }

        public void Throw()
        {
            Drop();

            Vector3 force = _cameraTransform.forward * _throwForce;

            _currentHoldingItem.Rigidbody.AddForce(force, ForceMode.Impulse);
        }

        private IEnumerator MoveItemToHoldingPoint()
        {
            while (_currentHoldingItem != null)
            {
                _currentHoldingItem.transform.localPosition = Vector3.MoveTowards(_currentHoldingItem.transform.localPosition, Vector3.zero, 0.05f);
                _currentHoldingItem.transform.localRotation = Quaternion.RotateTowards(_currentHoldingItem.transform.localRotation, Quaternion.identity, 1f);
                yield return null;
            }
        }
    }
}