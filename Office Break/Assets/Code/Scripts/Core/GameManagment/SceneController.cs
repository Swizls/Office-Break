using System;
using UnityEngine;
using IngameDebugConsole;
using UnityEngine.SceneManagement;

namespace OfficeBreak.Core
{
    public class SceneController : MonoBehaviour
    {
        public const int MAIN_MENU_BUILD_INDEX = 0;
        public const int START_LEVEL_BUILD_INDEX = 1;

        private static int _currentLevelIndex = 0;

        public static event Action SceneChanged;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R))
                RestartLevel();
        }

        public void RestartLevel() => LoadScene(_currentLevelIndex);

        public void LoadMainMenu()
        {
            _currentLevelIndex = MAIN_MENU_BUILD_INDEX;
            LoadScene();
        }

        public void LoadStartLevel()
        {
            _currentLevelIndex = START_LEVEL_BUILD_INDEX;
            LoadScene();
        }

        public void LoadNextLevel()
        {
            _currentLevelIndex++;

            if (_currentLevelIndex > SceneManager.sceneCountInBuildSettings)
                throw new ApplicationException("Trying load scene that is not in build");

            LoadScene();
        }

        [ConsoleMethod("switch_level", "switching levels", "index")]
        public static async void LoadScene(int buildIndex)
        {
            _currentLevelIndex = buildIndex;
            await SceneManager.LoadSceneAsync(_currentLevelIndex);
            SceneChanged?.Invoke();
        }

        private async void LoadScene()
        {
            await SceneManager.LoadSceneAsync(_currentLevelIndex);
            SceneChanged?.Invoke();
        }
    }
}