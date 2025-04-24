using OfficeBreak.Core.DamageSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace OfficeBreak.DustructionSystem
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(NavMeshObstacle))]
    public class Destructable : MonoBehaviour, IHitable
    {
        [SerializeField] private Health _health;
        [SerializeField] private GameObject _model;
        [SerializeField] private float _explosionForce = 10f;
        [SerializeField] private AudioClip _destroySFX;

        private Rigidbody _rigidbody;
        private AudioSource _audioSource;
        private NavMeshObstacle _navMeshObstacle;

        private GameObject _fracturedVersion;
        private List<Rigidbody> _fracturedPiecesRigibody;

        public event Action Destroyed;
        public event Action<IHitable> GotHit;

        public Transform ModelTransform => _model.transform;
        public Health Health => _health;
        public bool IsDestroyed => _health.Value <= 0;

        private void Awake()
        {
            _health.Initialize();

            _navMeshObstacle = GetComponent<NavMeshObstacle>();
            _audioSource = GetComponent<AudioSource>();
            _rigidbody = _model.GetComponent<Rigidbody>();

            _fracturedVersion = FractureHandler.Fracture(_model);
            _fracturedVersion.transform.parent = transform;
            _fracturedPiecesRigibody = _fracturedVersion.GetComponentsInChildren<Rigidbody>().ToList();
            _fracturedVersion.SetActive(false);
        }

        private void OnEnable() => _health.Died += OnObjectDestroy;

        private void OnDisable() => _health.Died -= OnObjectDestroy;

        private void OnObjectDestroy()
        {
            Destroyed?.Invoke();

            _navMeshObstacle.carving = false;

            _audioSource.clip = _destroySFX;
            _audioSource.Play();

            Destroy(_model);

            _fracturedVersion.transform.position = _model.transform.position;
            _fracturedVersion.transform.rotation = _model.transform.rotation;
            _fracturedPiecesRigibody.ForEach(piece => piece.AddExplosionForce(_explosionForce, _model.transform.position, 5f, 5f, ForceMode.Impulse));
            _fracturedVersion.SetActive(true);
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