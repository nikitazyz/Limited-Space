using UnityEngine;

namespace LevelSystem
{
    [CreateAssetMenu(menuName = "LevelSettingsAsset", fileName = "New Level Settings")]
    public class LevelSettingsAsset : ScriptableObject
    {
        [field: SerializeField]
        public int InitialJumps { get; private set; }
    }
}