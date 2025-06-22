using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace OfficeBreak
{
    public class MouseSensivitySlider : MonoBehaviour
    {
        public const string MOUSE_SENSIVITY_KEY = "mouseSensivity";

        private const float DEFAULT_MOUSE_SENSIVITY_VALUE = 1f;

        [SerializeField] private Slider _slider;

        private void Awake()
        {
            if (_slider == null)
                throw new NullReferenceException("Master volume slider is not setted");
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey(MOUSE_SENSIVITY_KEY))
            {
                PlayerPrefs.SetFloat(MOUSE_SENSIVITY_KEY, DEFAULT_MOUSE_SENSIVITY_VALUE);
                PlayerPrefs.Save();
            }

            float sensivityValue = PlayerPrefs.GetFloat(MOUSE_SENSIVITY_KEY);

            _slider.value = sensivityValue;
            _slider.onValueChanged.AddListener(UpdateMouseSensivity);
        }

        private void OnDestroy() => _slider.onValueChanged.RemoveAllListeners();

        private void UpdateMouseSensivity(float value)
        {
            PlayerPrefs.SetFloat(MOUSE_SENSIVITY_KEY, value);
            PlayerPrefs.Save();
        }
    }
}