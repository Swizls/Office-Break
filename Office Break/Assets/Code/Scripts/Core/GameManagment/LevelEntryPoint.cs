using FabroGames;
using OfficeBreak.Characters;
using OfficeBreak.DustructionSystem;
using OfficeBreak.DustructionSystem.UI;
using OfficeBreak.Spawners;
using UnityEngine;

namespace OfficeBreak.Core
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [Header("Systems")]
        [SerializeField] private DestructionTracker _destructionTracker;
        [SerializeField] private EnemySpawnController _enemySpawnController;
        [Space]
        [Header("UI")]
        [SerializeField] private DestructableHealthUI _destructableHealthUI;

        private void Start()
        {
            Player player = FindAnyObjectByType<Player>();
            GameManager gameManager = new GameManager(_enemySpawnController, player);
            ServiceLocator.Register<GameManager>(gameManager);

            _destructionTracker.Initialzie();
            _destructableHealthUI.Initialize(_destructionTracker.Destructables, player.transform);
            _enemySpawnController.Initialize(player);

        }

        private void OnDestroy()
        {
            ServiceLocator.Clear();
        }
    }
}