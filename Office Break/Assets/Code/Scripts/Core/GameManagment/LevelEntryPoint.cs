using OfficeBreak.Characters;
using OfficeBreak.Characters.Enemies.AI;
using OfficeBreak.Core.GameManagment;
using OfficeBreak.DestructionSystem;
using OfficeBreak.DestructionSystem.UI;
using OfficeBreak.Spawners;
using System.Linq;
using UnityEngine;

namespace OfficeBreak.Core
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _playerSpawnerTransform;
        [Header("UI")]
        [SerializeField] private Canvas _canvasPrefab;
        [SerializeField] private DestructableHealthUI _destructableHealthUIPrefab;
        [Header("Systems")]
        [SerializeField] private DestructionTracker _destructionTrackerPrefab;
        [SerializeField] private EnemySpawnController _enemySpawnControllerPrefab;

        private void Awake()
        {
            if (GameManager.Instance == null)
                SceneLoader.LoadScene(SceneLoader.MAIN_MENU_BUILD_INDEX);

            SceneLoader.SceneChanged += Initialize;
        }

        private void OnDestroy() => SceneLoader.SceneChanged -= Initialize;

        private void OnValidate()
        {
            if (_enemySpawnControllerPrefab.TryGetComponent(out EnemiesController com) == false)
                throw new System.Exception($"Component '{nameof(EnemiesController)} is required to exist on gameobject'");
        }

        private void Initialize()
        {
            Player player = SpawnPlayer();

            //Systems - references
            DestructionTracker tracker = Instantiate(_destructionTrackerPrefab);
            EnemySpawnController enemySpawnController = Instantiate(_enemySpawnControllerPrefab);

            //UI - references
            DestructableHealthUI destructableHealthUI = Instantiate(_destructableHealthUIPrefab);
            Instantiate(_canvasPrefab);

            //Systems - init
            tracker.Initialzie();
            enemySpawnController.Initialize(player, GameManager.Instance.Difficulty);
            enemySpawnController.GetComponent<EnemiesController>().Initialize(GameManager.Instance.Difficulty);
            FindAnyObjectByType<GameManager>().Intialize(enemySpawnController, player);

            //UI - init
            destructableHealthUI.Initialize(tracker.Destructables, player.transform);

            player.gameObject.AddComponent<Healer>();
            GameManager.Instance.StartGame();

            Destroy(gameObject);
        }

        private Player SpawnPlayer()
        {
            Quaternion rotation;
            Vector3 position;

            if (_playerSpawnerTransform == null)
            {
                rotation = Quaternion.identity;
                position =
                    FindObjectsByType<ElevatorDoors>(FindObjectsSortMode.None)
                    .ToList()
                    .Where(elevator => elevator.Type == ElevatorDoors.ElevatorType.Start)
                    .First().transform.position;
            }
            else
            {
                rotation = Quaternion.Euler(0, _playerSpawnerTransform.eulerAngles.y, 0);
                position = _playerSpawnerTransform.position;
            }

            return Instantiate(_playerPrefab, position, rotation);
        }
    }
}