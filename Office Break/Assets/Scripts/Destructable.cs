using System;
using UnityEngine;

namespace OfficeBreak
{
    [RequireComponent (typeof(Rigidbody))]
    [RequireComponent (typeof(AudioSource))]
    [RequireComponent (typeof(MeshRenderer))]
    public class Destructable : MonoBehaviour, IHitable
    {
        [SerializeField] private Health _health;

        private Rigidbody _rigidbody;
        private Collider _collider;
        private MeshRenderer _meshRenderer;
        private AudioSource _audioSource;

        public Action Destroyed;

        public bool IsDestroyed => _health.Value <= 0;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable() => _health.Died += OnObjectDestroy;

        private void OnDisable() => _health.Died -= OnObjectDestroy;

        private void OnObjectDestroy()
        {
            Destroyed?.Invoke();

            _rigidbody.isKinematic = true;

            _collider.enabled = false;
            _meshRenderer.enabled = false;

            _audioSource.Play();

            Destroy(this);

            FractureHandler.Fracture(gameObject);
        }

        public void TakeHit(HitData hitData)
        {
            _health.TakeDamage(hitData.Damage);
            _rigidbody.AddForce(hitData.HitDirection * hitData.AttackForce);
        }
    }
}