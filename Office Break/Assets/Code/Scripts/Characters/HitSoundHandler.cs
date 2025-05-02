using OfficeBreak.Core.DamageSystem;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeBreak.Characters
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(IHitable))]
    public class HitSoundHandler : MonoBehaviour
    {
        [SerializeField] private float _minPitch = 0.8f;
        [SerializeField] private float _maxPitch = 1.2f;
        [SerializeField] private List<AudioClip> _hitSFXs;

        private AudioSource _audioSource;
        private IHitable _hitable;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _hitable = GetComponent<IHitable>();
        }

        private void OnEnable() => _hitable.GotHit += OnHit;

        private void OnDisable() => _hitable.GotHit -= OnHit;

        private void OnHit(IHitable hitable)
        {
            _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
            _audioSource.clip = _hitSFXs[UnityEngine.Random.Range(0, _hitSFXs.Count)];
            _audioSource.Play();
        }
    }
}