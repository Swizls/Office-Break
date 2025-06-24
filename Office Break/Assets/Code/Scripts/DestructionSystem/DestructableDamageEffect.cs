using OfficeBreak.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OfficeBreak.DestructionSystem
{
    [Serializable]
    public class DestructableDamageEffect : IDisposable
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private Material _damageMaterial;

        private Destructable _target;
        private MeshRenderer _meshRenderer;

        private Coroutine _damageAnimation;

        public void Initialize(Destructable target, MeshRenderer renderer)
        {
            _target = target;
            _meshRenderer = renderer;

            _target.GotHit += StartAnimation;
        }

        public void Dispose() => _target.GotHit -= StartAnimation;

        private void StartAnimation(IHitable hitable)
        {
            if(_damageAnimation == null)
                _damageAnimation = _target.StartCoroutine(PlayDamageAnimation());
        }

        private IEnumerator PlayDamageAnimation()
        {
            List<Material> materials = new List<Material>();

            int damageMaterialIndex = 0;

            foreach(Material material in _meshRenderer.materials)
            {
                materials.Add(material);
                damageMaterialIndex++;
            }

            materials.Add(_damageMaterial);

            _meshRenderer.SetMaterials(materials);

            float progress = 0f;

            while (progress < _curve.keys.Last().time)
            {
                progress += Time.deltaTime;
                _meshRenderer.materials[damageMaterialIndex].SetFloat("_Strength", _curve.Evaluate(progress));
                yield return null;
            }

            _damageAnimation = null;
        }
    }
}