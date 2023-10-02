using System;
using LevelSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _continue;
        [SerializeField] private Button _mainMenu;

        private bool _isPause;
        public void SetPause(bool isPause)
        {
            _isPause = isPause;
            _panel.SetActive(isPause);
            Time.timeScale = isPause ? 0 : 1;
        }
        
        private void Awake()
        {
            SetPause(false);
            _continue.onClick.AddListener(ContinueClicked);
            _mainMenu.onClick.AddListener(MainMenuClicked);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (LevelSwitch.Instance.Switching)
                {
                    return;
                }
                SetPause(!_isPause);
            }
        }

        private void MainMenuClicked()
        {
            if (LevelSwitch.Instance.Switching)
            {
                return;
            }
            SetPause(false);
            LevelSwitch.Instance.Switch("Scenes/MainMenu");
        }

        private void ContinueClicked()
        {
            if (LevelSwitch.Instance.Switching)
            {
                return;
            }
            SetPause(false);
        }
    }
}
