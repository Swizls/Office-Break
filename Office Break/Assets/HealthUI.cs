using OfficeBreak.Characters;
using OfficeBreak.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeBreak.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Color _fullHealthColor;
        [SerializeField] private Color _lowHealthColor;

        private Health _playerHealth;
        private Image _image;

        private void Awake()
        {
            _playerHealth = FindAnyObjectByType<Player>().Health;
            _image = GetComponent<Image>();
            UpdateUI();
        }

        private void OnEnable() => _playerHealth.ValueChanged += UpdateUI;

        private void OnDisable() => _playerHealth.ValueChanged -= UpdateUI;

        private void UpdateUI() => _image.color = Color.Lerp(_lowHealthColor, _fullHealthColor, _playerHealth.Value / 100);
    }
}