using OfficeBreak.Characters;
using OfficeBreak.Core;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace OfficeBreak.UI
{
    public class HUDEffectsController : MonoBehaviour
    {
        private const float CHANGE_SPEED = 0.01f;

        private Vignette _vignette;
        private Health _playerHealth;

        private void Awake()
        {
            VolumeProfile volumeProfile = FindAnyObjectByType<Volume>().profile;
            _playerHealth = FindAnyObjectByType<Player>().Health;
            _vignette = (Vignette)volumeProfile.components.Where(component => component.GetType() == typeof(Vignette)).First();
            _vignette.intensity.value = 0;
        }

        private void OnEnable()
        {
            _playerHealth.ValueChanged += OnHealthValueChange;
            _playerHealth.Died += OnPlayerDeath;
        }

        private void OnDisable()
        {
            _playerHealth.ValueChanged -= OnHealthValueChange;
            _playerHealth.Died -= OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            StopAllCoroutines();
            StartCoroutine(PlayVignetteAnimation(0));
        }

        private void OnHealthValueChange()
        {
            if (_playerHealth.IsDead)
                return;

            StopAllCoroutines();
            StartCoroutine(PlayVignetteAnimation(Mathf.Abs((_playerHealth.LeftHealthPercentage / 100) - 1f)));
        }

        private IEnumerator PlayVignetteAnimation(float targetValue)
        {
            while(_vignette.intensity.value != targetValue)
            {
                _vignette.intensity.value = Mathf.MoveTowards(_vignette.intensity.value, targetValue, CHANGE_SPEED);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}