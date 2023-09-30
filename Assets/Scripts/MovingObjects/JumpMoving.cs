using System;
using LevelSystem;
using UnityEngine;

namespace MovingObjects
{
    public class JumpMoving : MonoBehaviour
    {
        [SerializeField] private Vector2 _offset;

        private Vector3 _initialPosition;
        private bool _isOffset;

        public bool IsOffset
        {
            get => _isOffset;
            private set
            {
                _isOffset = value;
                transform.position = _initialPosition + (Vector3)_offset * (_isOffset ? 1 : 0);
            }
        }
        
        private void Awake()
        {
            _initialPosition = transform.position;
            Player.Player.Jumped += () => IsOffset = !IsOffset;
            LevelReset.LevelRestarted += () => IsOffset = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)_offset);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + (Vector3)_offset, 0.1f);
        }
    }
}
