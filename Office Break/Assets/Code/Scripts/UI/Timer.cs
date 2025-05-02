using System.Collections;
using TMPro;
using UnityEngine;

namespace OfficeBreak
{
    public class Timer : MonoBehaviour
    {
        private TextMeshProUGUI _timerText;

        private void Awake() => _timerText = GetComponentInChildren<TextMeshProUGUI>();

        private void Start() => StartTimer();

        public void StartTimer() => StartCoroutine(CountTime());

        public IEnumerator CountTime()
        {
            float time = 0f;

            while (true)
            {
                time += Time.deltaTime;

                int minutes = Mathf.FloorToInt(time / 60);
                int seconds = Mathf.FloorToInt(time % 60);

                _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                yield return null;
            }
        }
    }
}