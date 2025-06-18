using OfficeBreak.Core;
using OfficeBreak.Core.Configs;
using OfficeBreak.Core.GameManagment;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfficeBreak
{
    public class StartScreenUI : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private DifficultyConfig[] _difficulties;

        private Dictionary<TMP_Dropdown.OptionData, DifficultyConfig> data = new Dictionary<TMP_Dropdown.OptionData, DifficultyConfig>();

        public void Awake()
        {
            _startButton.onClick.AddListener(StartGame);

            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

            foreach (DifficultyConfig config in _difficulties)
            {
                var option = new TMP_Dropdown.OptionData(config.name.Split()[0]);
                data.Add(option, config);
                options.Add(option);
            }

            _dropdown.AddOptions(options);           
        }

        private void OnDestroy() => _startButton.onClick.RemoveListener(StartGame);

        private void StartGame()
        {
            SetDifficulty();
            GameManager.Instance.GetComponent<SceneLoader>().LoadStartLevel();
        }

        private void SetDifficulty()
        {
            if(data.TryGetValue(_dropdown.options[_dropdown.value], out DifficultyConfig config))
            {
                GameManager.Instance.SetDifficulty(config);
            }
        }
    }
}
