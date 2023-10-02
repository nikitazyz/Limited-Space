using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelSystem
{
    public class LevelSwitchTrigger : MonoBehaviour
    {
        [SerializeField] private string _nextLevel;

        private void Awake()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player.Player plr))
            {
                LevelSwitch.Instance.Switch(_nextLevel);
            }
        }
    }
}