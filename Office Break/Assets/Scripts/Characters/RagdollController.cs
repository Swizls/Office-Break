using UnityEngine;
using UnityEngine.Events;

namespace OfficeBreak.Characters
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] protected GameObject _rootBone;

        private Collider[] _colliders;
        private Rigidbody[] _rigidbodies;

        public UnityEvent OnRagdollEnable;
        public UnityEvent OnRagdollDisable;

        private void Awake()
        {
            _colliders = _rootBone.GetComponentsInChildren<Collider>();
            _rigidbodies = _rootBone.GetComponentsInChildren<Rigidbody>();

            DisableRagdoll();
        }

        public virtual void EnableRagdoll()
        {
            foreach (Collider collider in _colliders)
                collider.enabled = true;

            foreach (Rigidbody rigidbody in _rigidbodies)
                rigidbody.isKinematic = false;

            OnRagdollEnable?.Invoke();
        }

        public virtual void DisableRagdoll()
        {
            foreach (Collider collider in _colliders)
                collider.enabled = false;

            foreach (Rigidbody rigidbody in _rigidbodies)
                rigidbody.isKinematic = true;

            OnRagdollDisable?.Invoke();
        }
    }
}