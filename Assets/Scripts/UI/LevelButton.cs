using System;
using LevelSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _number;
        public string level;

        public string Text
        {
            get => _number.text;
            set => _number.text = value;
        }
        private void Awake()
        {
            _button.onClick.AddListener(Clicked);
        }

        private void Clicked()
        {
            LevelSwitch.Instance.Switch(level);
        }
    }
}
