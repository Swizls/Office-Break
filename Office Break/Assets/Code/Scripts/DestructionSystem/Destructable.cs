using OfficeBreak.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace OfficeBreak.DestructionSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class Destructable : MonoBehaviour, IHitable
    {
        [SerializeField] private Health _health;
        [SerializeField] private GameObject _model;
        [SerializeField] private float _explosionForce = 10f;
        [Space]
        [Header("Audio")]
        [SerializeField] private AudioClip[] _hitSFXes;
        [SerializeField] private AudioClip _destroySFX;

        private Rigidbody _rigidbody;
        private AudioSource _audioSource;
        private SFXPlayer _sfxPlayer;

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

            _audioSource = GetComponent<AudioSource>();
            _rigidbody = _model.GetComponent<Rigidbody>();

            _sfxPlayer = new SFXPlayer(_audioSource);
            _sfxPlayer.AddClip(nameof(_destroySFX), _destroySFX);
            _sfxPlayer.AddClips(nameof(_hitSFXes), _hitSFXes);

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

            _sfxPlayer.Play(nameof(_destroySFX));

            Destroy(_model);

            _fracturedVersion.transform.position = _model.transform.position;
            _fracturedVersion.transform.rotation = _model.transform.rotation;
            _fracturedVersion.SetActive(true);
            _fracturedPiecesRigibody.ForEach(piece => piece.AddExplosionForce(_explosionForce, _fracturedVersion.transform.position, 1f, 1f, ForceMode.Force));
        }

        public void TakeHit(HitData hitData)
        {
            _rigidbody.isKinematic = false;
            _health.TakeDamage(hitData.Damage);
            _rigidbody.AddForce(hitData.HitDirection * hitData.AttackForce);

            if(!_health.IsDead)
            {
                _sfxPlayer.Play(nameof(_hitSFXes));
                GotHit?.Invoke(this);
            }
        }
    }
}