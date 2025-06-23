using OfficeBreak.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OfficeBreak.DestructionSystem
{
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class Destructable : MonoBehaviour, IHitable
    {
        [SerializeField] private Health _health;
        [SerializeField] private GameObject _model;
        [SerializeField] private float _explosionForce = 10f;
        [Space]
        [Header("Audio")]
        [SerializeField] private AudioClip[] _hitSFXes;
        [SerializeField] private AudioClip _destroySFX;

        private MeshRenderer _modelMeshRenderer;
        private Collider[] _modelColliders;
        private Rigidbody _modelRigidbody;
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

            if (_model == null)
                _model = gameObject;

            _modelRigidbody = _model.GetComponent<Rigidbody>();
            _modelMeshRenderer = _model.GetComponent<MeshRenderer>();
            _modelColliders = _model.GetComponents<Collider>();
        }

        public void Initialize()
        {
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
            bool isScriptAttachedToModel = _model.gameObject == gameObject;

            Destroyed?.Invoke();

            _sfxPlayer.Play(nameof(_destroySFX));

            if (isScriptAttachedToModel)
            {
                _modelMeshRenderer.enabled = false;
                foreach(Collider collider in _modelColliders)
                    collider.enabled = false;

                _modelRigidbody.isKinematic = true;
            }
            else
            {
                Destroy(_model);
            }

            _fracturedVersion.transform.position = _model.transform.position;
            _fracturedVersion.transform.rotation = _model.transform.rotation;
            _fracturedVersion.SetActive(true);
            _fracturedPiecesRigibody.ForEach(piece => piece.AddExplosionForce(_explosionForce, _fracturedVersion.transform.position, 1f, 0, ForceMode.Force));
        }

        public void TakeHit(HitData hitData)
        {
            _modelRigidbody.isKinematic = false;
            _health.TakeDamage(hitData.Damage);
            _modelRigidbody.AddForce(hitData.HitDirection * hitData.AttackForce);

            if(!_health.IsDead)
            {
                _sfxPlayer.Play(nameof(_hitSFXes));
                GotHit?.Invoke(this);
            }
        }
    }
}