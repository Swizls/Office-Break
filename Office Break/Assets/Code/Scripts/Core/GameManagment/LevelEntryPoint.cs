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
        [SerializeField] private Canvas _canvasPrefab;
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

            //Systems - references
            DestructionTracker tracker = Instantiate(_destructionTrackerPrefab);
            EnemySpawnController enemySpawnController = Instantiate(_enemySpawnControllerPrefab);

            //UI - references
            DestructableHealthUI destructableHealthUI = Instantiate(_destructableHealthUIPrefab);
            Instantiate(_canvasPrefab);

            //Systems - init
            tracker.Initialzie();
            enemySpawnController.Initialize(player);
            FindAnyObjectByType<GameManager>().Intialize(enemySpawnController, player);

            //UI - init
            destructableHealthUI.Initialize(tracker.Destructables, player.transform);

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