using OfficeBreak.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OfficeBreak.DestructionSystem
{
    [RequireComponent(typeof(Destructable))]
    public class DamageEffect : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private Material _damageMaterial;

        private Destructable _target;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _target = GetComponent<Destructable>();
            _meshRenderer = _target.GetComponentInChildren<MeshRenderer>();
        }

        private void OnEnable() => _target.GotHit += StartAnimation;

        private void OnDisable() => _target.GotHit -= StartAnimation;

        private void StartAnimation(IHitable hitable) => StartCoroutine(PlayDamageAnimation());

        private IEnumerator PlayDamageAnimation()
        {
            List<Material> materials = new List<Material>{
                _meshRenderer.materials[0],
                _damageMaterial,
            };
            _meshRenderer.SetMaterials(materials);

            float progress = 0f;

            while (progress < _curve.keys.Last().time)
            {
                progress += Time.deltaTime;
                _meshRenderer.materials[1].SetFloat("_Strength", _curve.Evaluate(progress));
                yield return null;
            }
        }
    }
}