using UnityEngine;
using UnityEngine.SceneManagement;

namespace OfficeBreak.Core
{
    public class SceneManagerTest : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}