using UnityEngine;
using UnityEngine.Events;

namespace OfficeBreak.Characters
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] private GameObject _rootBone;

        private Rigidbody[] _rigidbodies;

        public UnityEvent OnRagdollEnable;
        public UnityEvent OnRagdollDisable;

        public Vector3 RootBonePosition => _rootBone.transform.position;

        private void Awake()
        {
            _rigidbodies = _rootBone.GetComponentsInChildren<Rigidbody>();

            DisableRagdoll();
        }

        public virtual void EnableRagdoll()
        {
            foreach (Rigidbody rigidbody in _rigidbodies)
                rigidbody.isKinematic = false;

            OnRagdollEnable?.Invoke();
        }

        public virtual void DisableRagdoll()
        {
            foreach (Rigidbody rigidbody in _rigidbodies)
                rigidbody.isKinematic = true;

            OnRagdollDisable?.Invoke();
        }

        public void ApplyForce(Vector3 force)
        {
            _rootBone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
}