using OfficeBreak.Characters;
using OfficeBreak.Characters.Enemies.AI;
using OfficeBreak.DestructionSystem;
using OfficeBreak.DestructionSystem.UI;
using OfficeBreak.Spawners;
using UnityEngine;

namespace OfficeBreak.Core
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [Header("Prefabs")]
        [Header("UI")]
        [SerializeField] private DestructionLevelUI _destructionLevelUI;
        [SerializeField] private DestructableHealthUI _destructableHealthUIPrefab;
        [Header("Systems")]
        [SerializeField] private DestructionTracker _destructionTrackerPrefab;
        [SerializeField] private EnemySpawnController _enemySpawnControllerPrefab;

        private void Awake()
        {
            if (GameManager.Instance == null)
                SceneController.LoadScene(SceneController.MAIN_MENU_BUILD_INDEX);

            SceneController.SceneChanged += Initialize;
        }

        private void Initialize()
        {
            Player player = FindAnyObjectByType<Player>();
            Canvas canvas = FindAnyObjectByType<Canvas>();

            //Systems - references
            DestructionTracker tracker = Instantiate(_destructionTrackerPrefab);
            EnemySpawnController enemySpawnController = Instantiate(_enemySpawnControllerPrefab);

            //UI - references
            DestructableHealthUI destructableHealthUI = Instantiate(_destructableHealthUIPrefab);
            DestructionLevelUI meter = Instantiate(_destructionLevelUI, canvas.transform);

            //Systems - init
            tracker.Initialzie();
            enemySpawnController.Initialize(player);
            FindAnyObjectByType<GameManager>().Intialize(enemySpawnController, player);

            //UI - init
            destructableHealthUI.Initialize(tracker.Destructables, player.transform);
            meter.gameObject.SetActive(true);

            player.gameObject.AddComponent<Healer>();

            Destroy(gameObject);
        }

        private void OnDestroy() => SceneController.SceneChanged -= Initialize;

        private void OnValidate()
        {
            if (_enemySpawnControllerPrefab.TryGetComponent(out EnemiesController com) == false)
                throw new System.Exception($"Component '{nameof(EnemiesController)} is required to exist on gameobject'");
        }
    }
}