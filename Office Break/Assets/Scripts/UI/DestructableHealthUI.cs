using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OfficeBreak.UI
{
    public class DestructableHealthUI : MonoBehaviour
    {
        private const float HEALTH_BAR_ANIMATION_SPEED = 0.1f;
        private const float RED_BAR_ANIMATION_DELAY = 0.1f;
        private const float FOLLOW_SPEED = 0.1f;
        private const float DISTANCE_TO_DISAPPEAR = 5f;

        private IReadOnlyList<Destructable> _destructables;
        private Destructable _currentDestructableObject;
        private Transform _playerTransform;

        [SerializeField] private RectTransform _healthBarRectTransform;
        [SerializeField] private RectTransform _redBarTransform;

        public void Initialize(IReadOnlyList<Destructable> destructables, Transform playerTransform)
        {
            _destructables = destructables;
            _playerTransform = playerTransform;

            foreach (Destructable destructable in _destructables)
                destructable.GotHit += OnHit;
         
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            foreach (var destructable in _destructables)
                destructable.GotHit -= OnHit;
        }

        private void OnDrawGizmos()
        {
            if (_currentDestructableObject == null)
                return;

            Gizmos.DrawSphere(CalculatePointBetweenObjectAndPlayer(_currentDestructableObject.transform.position), 0.5f);
        }

        private void OnHit(Destructable destructable)
        {
            gameObject.SetActive(true);

            if(destructable != _currentDestructableObject)
            {
                _currentDestructableObject = destructable;
                _currentDestructableObject.Destroyed += OnDestructableDestroy;
                SetHealthBarValue();
            }

            transform.position = CalculatePointBetweenObjectAndPlayer(destructable.transform.position);
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);

            StartCoroutine(PlayHealthBarAnimation(_currentDestructableObject.Health.LeftHealthPercentage));
            StartCoroutine(PlayRedBarAnimation(_currentDestructableObject.Health.LeftHealthPercentage));
            StartCoroutine(FollowPlayer());
        }

        private void OnDestructableDestroy()
        {
            _currentDestructableObject.Destroyed -= OnDestructableDestroy;
            _currentDestructableObject = null;
            StopAllCoroutines();
            gameObject.SetActive(false);
        }

        private void SetHealthBarValue()
        {
            _healthBarRectTransform.localScale = new Vector3(_currentDestructableObject.Health.LeftHealthPercentage / 100, _healthBarRectTransform.localScale.y, _healthBarRectTransform.localScale.z);
            _redBarTransform.localScale = new Vector3(1, _redBarTransform.localScale.y, _redBarTransform.localScale.z);
        }

        private Vector3 CalculatePointBetweenObjectAndPlayer(Vector3 objectPosition)
        {
            return (objectPosition - _playerTransform.position) / 2 + _playerTransform.position + Vector3.up * 0.5f;
        }

        private IEnumerator FollowPlayer()
        {
            while (true)
            {
                if (_currentDestructableObject.IsDestroyed)
                    break;

                if (Vector3.Distance(_playerTransform.position, _currentDestructableObject.transform.position) > DISTANCE_TO_DISAPPEAR)
                    break;

                transform.position = Vector3.Lerp(transform.position, CalculatePointBetweenObjectAndPlayer(_currentDestructableObject.transform.position), FOLLOW_SPEED);
                transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
                yield return new WaitForEndOfFrame();
            }

            gameObject.SetActive(false);
        }

        private IEnumerator PlayHealthBarAnimation(float target)
        {
            if (target < 0)
                target = 0;

            Vector3 targetScale = new Vector3(target / 100, _healthBarRectTransform.localScale.y, _healthBarRectTransform.localScale.z);
            while (Vector3.Distance(_healthBarRectTransform.localScale, targetScale) > 0.01f)
            {
                _healthBarRectTransform.localScale = Vector3.Lerp(_healthBarRectTransform.localScale, targetScale, HEALTH_BAR_ANIMATION_SPEED);
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator PlayRedBarAnimation(float target)
        {
            yield return new WaitForSeconds(RED_BAR_ANIMATION_DELAY);

            if (target < 0)
                target = 0;

            Vector3 targetScale = new Vector3(target / 100, _healthBarRectTransform.localScale.y, _healthBarRectTransform.localScale.z);
            while (Vector3.Distance(_redBarTransform.localScale, targetScale) > 0.01f)
            {
                _redBarTransform.localScale = Vector3.Lerp(_redBarTransform.localScale, targetScale, HEALTH_BAR_ANIMATION_SPEED);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}