using OfficeBreak.UI;
using UnityEngine;

namespace OfficeBreak.Core
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [Header("Systems")]
        [SerializeField] private DestructionTracker _destructionTracker;
        [Space]
        [Header("Player")]
        [SerializeField] private Transform _playerTransform;
        [Space]
        [Header("UI")]
        [SerializeField] private DestructableHealthUI _destructableHealthUI;

        private void Start()
        {
            _destructionTracker.Initialzie();
            _destructableHealthUI.Initialize(_destructionTracker.Destructables, _playerTransform);
        }
    }
}