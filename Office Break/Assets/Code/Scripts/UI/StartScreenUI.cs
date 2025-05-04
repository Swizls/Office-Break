using FabroGames;
using OfficeBreak.Core;
using UnityEngine;

namespace OfficeBreak
{
    public class StartScreenUI : MonoBehaviour
    {
        public void StartGame()
        {
            GameManager.Instance.StartGame();
            gameObject.SetActive(false);
        }
    }
}
