using System;
using LevelSystem;
using UnityEngine;

namespace UI
{
    public class LevelsButtonGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private LevelButton _template;
        [SerializeField] private LevelsAsset _levelsAsset;

        private void Start()
        {
            int i = 0;
            foreach (var level in _levelsAsset.Levels)
            {
                
                var instance = Instantiate(_template, _root);
                instance.level = _levelsAsset.LevelsRoot + level;
                instance.Text = $"{++i}";
                instance.gameObject.SetActive(true);
            }
        }
    }
}