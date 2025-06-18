using FabroGames.PlayerControlls;
using OfficeBreak.Characters;
using OfficeBreak.Characters.Animations;
using OfficeBreak.Characters.FightingSystem;
using OfficeBreak.Core.Configs;
using OfficeBreak.DestructionSystem;
using OfficeBreak.Spawners;
using System;
using System.Linq;
using UnityEngine;

namespace OfficeBreak.Core.GameManagment
{
    public class GameManager : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        private EnemySpawnController _enemySpawnController;
        private IMovable _playerMovement;
        private PlayerCamera _playerCamera;
        private AttackController _playerAttackController;
        private AnimatorController _animatorController;
        private DestructionTracker _destructionTracker;

        public event Action GameInitialized;
        public event Action LevelCompleted;

        public static GameManager Instance { get; private set; }
        [field: SerializeField] public DifficultyConfig Difficulty { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _sceneLoader = GetComponent<SceneLoader>();
            DontDestroyOnLoad(gameObject);
        }

        public void Intialize(EnemySpawnController enemySpawnController, Player player)
        {
            _enemySpawnController = enemySpawnController;
            _playerMovement = player.GetComponent<IMovable>();
            _playerAttackController = player.GetComponent<AttackController>();
            _playerCamera = player.GetComponentInChildren<PlayerCamera>();
            _animatorController = player.GetComponentInChildren<AnimatorController>();
            _destructionTracker = FindAnyObjectByType<DestructionTracker>();

            _enemySpawnController.enabled = false;
            _playerMovement.Enabled = false;
            _playerCamera.enabled = false;
            _playerAttackController.enabled = false;
            _animatorController.enabled = false;

            GameInitialized?.Invoke();
        }

        public void StartGame()
        {
            _enemySpawnController.enabled = true;
            _playerMovement.Enabled = true;
            _playerCamera.enabled = true;
            _playerAttackController.enabled = true;
            _animatorController.enabled = true;

            _destructionTracker.LevelDestroyed += OnLevelDestroy;

            _enemySpawnController.SpawnFirstWave();
        }

        public void SetDifficulty(DifficultyConfig config) => Difficulty = config;

        private void OnLevelDestroy()
        {
            ElevatorDoors elevator =
                FindObjectsByType<ElevatorDoors>(FindObjectsSortMode.None)
                .Where(obj => obj.Type == ElevatorDoors.ElevatorType.Exit)
                .First();
            elevator.Open();

            ExitTrigger trigger = FindAnyObjectByType<ExitTrigger>();

            trigger.PlayerEnteredExit += OnExitEnter;

            LevelCompleted?.Invoke();
        }

        private void OnExitEnter() => _sceneLoader.LoadNextLevel();
    }
}