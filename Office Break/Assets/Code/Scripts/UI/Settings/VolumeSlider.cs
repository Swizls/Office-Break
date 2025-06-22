using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace OfficeBreak
{
    public class VolumeSlider : MonoBehaviour
    {
        private const string MASTER_VOLUME_PARAMETER_NAME = "masterVolume";

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _volumeSlider;

        private float MasterVolumeSavedValue => PlayerPrefs.GetFloat(MASTER_VOLUME_PARAMETER_NAME);

        private void Awake()
        {
            if(_audioMixer == null)
                throw new NullReferenceException("Audio mixer is not setted");

            if (_volumeSlider == null)
                throw new NullReferenceException("Master volume slider is not setted");
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey(MASTER_VOLUME_PARAMETER_NAME))
            {
                PlayerPrefs.SetFloat(MASTER_VOLUME_PARAMETER_NAME, _volumeSlider.value);
                PlayerPrefs.Save();
            }

            UpdateMasterVolume(MasterVolumeSavedValue);
            _volumeSlider.value = MasterVolumeSavedValue;
            _volumeSlider.onValueChanged.AddListener(UpdateMasterVolume);
        }

        private void OnDestroy() => _volumeSlider.onValueChanged.RemoveAllListeners();

        private void UpdateMasterVolume(float value)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_PARAMETER_NAME, value);
            PlayerPrefs.Save();

            _audioMixer.SetFloat(MASTER_VOLUME_PARAMETER_NAME, value);
        }
    }
}