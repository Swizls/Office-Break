using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeBreak.DestructionSystem
{
    public class DestructableHighlighter : MonoBehaviour
    {
        private const float OUTLINE_DISSAPEAR_SPEED = 2f;
        [SerializeField] private Material _highlighteMaterial;

        private Destructable _destructabe;
        private MeshRenderer _renderer;
        private Material _defaultMaterial;

        private Coroutine _highlighteRoutine;

        private void Awake()
        {
            _destructabe = GetComponent<Destructable>();
            _renderer = GetComponentInChildren<MeshRenderer>();

            _highlighteMaterial = new Material(_highlighteMaterial);
            _defaultMaterial = _renderer.material;
        }

        private void OnEnable() => _destructabe.Health.Died += () => Destroy(this);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Highlighte();
            }
        }

        public void Highlighte()
        {
            if(_highlighteRoutine != null)
                return;

            _highlighteRoutine = StartCoroutine(HighlighteAnimation());
        }

        private IEnumerator HighlighteAnimation()
        {
            _renderer.SetMaterials(new List<Material>(){ _defaultMaterial, _highlighteMaterial});

            Color startColor = _highlighteMaterial.GetColor("_OutlineColor");
            Color targetColor = _highlighteMaterial.GetColor("_OutlineColor");
            targetColor.a = 0;

            while(_highlighteMaterial.GetColor("_OutlineColor").a > 0.1f)
            {
                _highlighteMaterial.SetColor("_OutlineColor", Color.Lerp(_highlighteMaterial.GetColor("_OutlineColor"), targetColor, OUTLINE_DISSAPEAR_SPEED * Time.deltaTime));
                yield return null;
            }

            _highlighteMaterial.SetColor("_OutlineColor", startColor);

            _renderer.SetMaterials(new List<Material>() { _defaultMaterial });
            _highlighteRoutine = null;
        }
    }
}