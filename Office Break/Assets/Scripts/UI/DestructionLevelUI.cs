using System.Collections;
using UnityEngine;

namespace OfficeBreak.UI
{
    public class DestructionLevelUI : MonoBehaviour
    {
        private const float ANIMATION_SPEED = 0.01f;

        [SerializeField] private RectTransform _barTransform;
        [SerializeField] private DestructionTracker _destructionTracker;

        private void Start ()
        {
            UpdateValue();
        }

        private void OnEnable()
        {
            _destructionTracker.DestructablesUpdated += UpdateValue;
        }

        private void OnDisable()
        {
            _destructionTracker.DestructablesUpdated -= UpdateValue;
        }

        private void UpdateValue()
        {
            StartCoroutine(PlayAnimation(_destructionTracker.DestructionLevelByPercent));
        }

        private IEnumerator PlayAnimation(float target)
        {
            Vector3 targetScale = new Vector3(target / 100, _barTransform.localScale.y, _barTransform.localScale.z);
            while (Vector3.Distance(_barTransform.localScale, targetScale) > 0.1f)
            {
                _barTransform.localScale = Vector3.Lerp(_barTransform.localScale, targetScale, ANIMATION_SPEED);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}