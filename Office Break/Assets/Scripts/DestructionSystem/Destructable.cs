using OfficeBreak.Core.DamageSystem;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace OfficeBreak.DustructionSystem
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(NavMeshObstacle))]
    public class Destructable : MonoBehaviour, IHitable
    {
        [SerializeField] private Health _health;
        [SerializeField] private AudioClip _destroySFX;

        private Rigidbody _rigidbody;
        private Collider _collider;
        private MeshRenderer _meshRenderer;
        private AudioSource _audioSource;
        private NavMeshObstacle _navMeshObstacle;

        public event Action Destroyed;
        public event Action<IHitable> GotHit;

        public bool IsDestroyed => _health.Value <= 0;
        public Health Health => _health;

        private void Awake()
        {
            _health.Initialize();

            _rigidbody = GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();
            _audioSource = GetComponent<AudioSource>();
            _navMeshObstacle = GetComponent<NavMeshObstacle>();
        }

        private void OnEnable() => _health.Died += OnObjectDestroy;

        private void OnDisable() => _health.Died -= OnObjectDestroy;

        private void OnDestroy() => Destroyed?.Invoke();

        private void OnObjectDestroy()
        {
            _rigidbody.isKinematic = true;

            _collider.enabled = false;
            _meshRenderer.enabled = false;
            _navMeshObstacle.enabled = false;

            _audioSource.clip = _destroySFX;
            _audioSource.Play();

            Destroy(this);

            FractureHandler.Fracture(gameObject);
        }

        public void TakeHit(HitData hitData)
        {
            GotHit?.Invoke(this);

            _rigidbody.isKinematic = false;
            _health.TakeDamage(hitData.Damage);
            _rigidbody.AddForce(hitData.HitDirection * hitData.AttackForce);
        }
    }
}