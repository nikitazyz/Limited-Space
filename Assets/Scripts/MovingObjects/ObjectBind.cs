using System;
using System.Collections.Generic;
using LevelSystem;
using UnityEngine;

namespace MovingObjects
{
    public class ObjectBind : MonoBehaviour
    {
        private bool isReseting;
        
        private void Awake()
        {
            LevelReset.LevelRestartStarted += OnLevelResetOnLevelRestartStarted;

            LevelReset.LevelRestarted += OnLevelResetOnLevelRestarted;
        }

        private void OnDestroy()
        {
            LevelReset.LevelRestarted -= OnLevelResetOnLevelRestarted;
            LevelReset.LevelRestartStarted -= OnLevelResetOnLevelRestartStarted;
        }

        private void OnLevelResetOnLevelRestarted()
        {
            isReseting = false;
        }

        private void OnLevelResetOnLevelRestartStarted()
        {
            isReseting = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.SetParent(null);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isReseting)
            {
                return;
            }
            var rb = other.attachedRigidbody;
            if (!rb)
            {
                return;
            }
            
            rb.transform.SetParent(transform);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            var rb = other.attachedRigidbody;
            if (!rb || rb.transform.parent != transform)
            {
                return;
            }
            
            rb.transform.SetParent(null);
        }
    }
}
