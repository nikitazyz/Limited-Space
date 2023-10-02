using System;
using LevelSystem;
using UnityEngine;

namespace Player
{
    public class JumpBonus : MonoBehaviour
    {
        private void Awake()
        {
            LevelReset.LevelRestarted += LevelResetOnLevelRestarted;
        }

        private void OnDestroy()
        {
            LevelReset.LevelRestarted -= LevelResetOnLevelRestarted;
        }

        private void LevelResetOnLevelRestarted()
        {
            gameObject.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player player))
            {
                return;
            }

            player.Jumps++;
            gameObject.SetActive(false);
        }
    }
}
