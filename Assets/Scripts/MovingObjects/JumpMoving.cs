using System;
using LevelSystem;
using UnityEngine;

namespace MovingObjects
{
    public class JumpMoving : MonoBehaviour
    {
        [SerializeField] private Vector2 _offset;
        [SerializeField] private float _speed;

        private Vector3 _initialPosition;

        public bool IsOffset { get; private set; }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                _initialPosition + (Vector3)_offset * (IsOffset ? 1 : 0), _speed * Time.deltaTime);
        }

        private void Awake()
        {
            _initialPosition = transform.position;
            Player.Player.Jumped += () => IsOffset = !IsOffset;
            LevelReset.LevelRestarted += OnLevelResetOnLevelRestarted;
        }

        private void OnLevelResetOnLevelRestarted()
        {
            IsOffset = false;
            transform.position = _initialPosition;
        }

        private void OnDestroy()
        {
            LevelReset.LevelRestarted -= OnLevelResetOnLevelRestarted;
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
