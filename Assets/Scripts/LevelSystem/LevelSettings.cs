using System;
using UnityEngine;

namespace LevelSystem
{
    public class LevelSettings : MonoBehaviour
    {
        private static LevelSettings _settingsInstance;

        public static LevelSettingsAsset Settings => _settingsInstance._levelSettings;
        
        [SerializeField] private LevelSettingsAsset _levelSettings;

        private void Awake()
        {
            _settingsInstance = this;
        }

        private void OnDestroy()
        {
            _settingsInstance = null;
        }
    }
}
