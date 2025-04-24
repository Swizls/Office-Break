using FabroGames;
using OfficeBreak.Core;
using UnityEngine;

namespace OfficeBreak
{
    public class StartScreenUI : MonoBehaviour
    {
        public void StartGame()
        {
            ServiceLocator.Get<GameManager>().StartGame();
            gameObject.SetActive(false);
        }
    }
}
