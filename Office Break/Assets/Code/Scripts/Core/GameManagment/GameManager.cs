using FabroGames;
using FabroGames.PlayerControlls;
using OfficeBreak.Characters;
using OfficeBreak.Characters.Animations;
using OfficeBreak.Characters.FightingSystem;
using OfficeBreak.Spawners;

namespace OfficeBreak.Core
{
    public class GameManager : IService
    { 
        private EnemySpawnController _enemySpawnController;
        private IMovable _playerMovement;
        private PlayerCamera _playerCamera;
        private AttackController _playerAttackController;
        private AnimatorController _animatorController;

        public GameManager(EnemySpawnController enemySpawnController, Player player)
        {
            _enemySpawnController = enemySpawnController;
            _playerMovement = player.GetComponent<IMovable>();
            _playerAttackController = player.GetComponent<AttackController>();
            _playerCamera = player.GetComponentInChildren<PlayerCamera>();
            _animatorController = player.GetComponentInChildren<AnimatorController>();

            _enemySpawnController.enabled = false;
            _playerMovement.Enabled = false;
            _playerCamera.enabled = false;
            _playerAttackController.enabled = false;
            _animatorController.enabled = false;
        }

        public void StartGame()
        {
            _enemySpawnController.enabled = true;
            _playerMovement.Enabled = true;
            _playerCamera.enabled = true;
            _playerAttackController.enabled = true;
            _animatorController.enabled = true;

            _enemySpawnController.SpawnFirstWave();
        }
    }
}