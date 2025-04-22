using System.Collections;
using UnityEngine;

namespace OfficeBreak.DustructionSystem.UI
{
    public class DestructionLevelUI : MonoBehaviour
    {
        private const float ANIMATION_SPEED = 0.001f;

        [SerializeField] private RectTransform _barTransform;
        [SerializeField] private DestructionTracker _destructionTracker;

        private void Start()
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
            while (Vector3.Distance(_barTransform.localScale, targetScale) > 0.01f)
            {
                _barTransform.localScale = Vector3.MoveTowards(_barTransform.localScale, targetScale, ANIMATION_SPEED);
                yield return new WaitForEndOfFrame();
            }

            if (target == 100)
                _barTransform.localScale.Set(1, _barTransform.localScale.y, _barTransform.localScale.z);
        }
    }
}