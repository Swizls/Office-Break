using OfficeBreak.Characters.FightingSystem;
using UnityEngine;

namespace OfficeBreak.Characters
{
    [RequireComponent(typeof(Animator))]
    public abstract partial class RagdollController : MonoBehaviour
    {
        [SerializeField] protected GameObject _rootBone;
        [SerializeField] protected CharacterController _characterController;

        private Animator _animator;
        private AttackController _attackController;

        private Collider[] _colliders;
        private Rigidbody[] _rigidbodies;

        private void Awake() => Initialize();

        protected virtual void Initialize()
        {
            _animator = GetComponent<Animator>();

            _colliders = _rootBone.GetComponentsInChildren<Collider>();
            _rigidbodies = _rootBone.GetComponentsInChildren<Rigidbody>();

            _attackController = GetComponentInParent<AttackController>();

            DisableRagdoll();
        }

        public virtual void EnableRagdoll()
        {
            foreach (Collider collider in _colliders)
            {
                collider.enabled = true;
            }

            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = false;
            }

            _animator.enabled = false;
            _characterController.enabled = false;
            _attackController.enabled = false;
        }

        public virtual void DisableRagdoll()
        {
            foreach (Collider collider in _colliders)
            {
                collider.enabled = false;
            }

            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = true;
            }

            _animator.enabled = true;
            _characterController.enabled = true;
            _attackController.enabled = true;
        }
    }
}