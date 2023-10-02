using System.Collections.Generic;
using UnityEngine;

namespace LevelSystem
{
    [CreateAssetMenu(menuName = "Levels Asset", fileName = "New Levels Asset")]
    public class LevelsAsset : ScriptableObject
    {
        [SerializeField] private string _levelsRoot;
        [SerializeField] private List<string> _levels;
        public string[] Levels => _levels.ToArray();

        public string LevelsRoot => _levelsRoot;
    }
}
