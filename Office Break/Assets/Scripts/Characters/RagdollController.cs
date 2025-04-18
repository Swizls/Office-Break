using OfficeBreak.Characters.Enemies;
using UnityEngine;

namespace OfficeBreak.Characters
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] private GameObject _rootBone;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyMover _enemyMover;

        private Collider[] _colliders;
        private Rigidbody[] _rigidbodies;

        private void Awake()
        {
            _colliders = _rootBone.GetComponentsInChildren<Collider>();
            _rigidbodies = _rootBone.GetComponentsInChildren<Rigidbody>();

            DisableRagdoll();
        }

        public void EnableRagdoll()
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
            _enemyMover.enabled = false;
        }

        public void DisableRagdoll()
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
            _enemyMover.enabled = true;
        }
    }
}