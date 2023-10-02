using LevelSystem;
using UnityEngine;

namespace MovingObjects
{
    public class ElevatorMoving : MonoBehaviour
    {
        [SerializeField] private Vector2 _offset;
        [SerializeField] private float _speed;
        [SerializeField] private PlayerStandDetector _playerStandDetector;

        private bool _isRiding;
        private bool _toOffset = true;
        private Vector3 _initialPosition;
        
        void Start()
        {
            _initialPosition = transform.position;
            LevelReset.LevelRestarted += LevelResetOnLevelRestarted;
        }

        private void LevelResetOnLevelRestarted()
        {
            _toOffset = true;
            transform.position = _initialPosition;
        }

        private void OnDestroy()
        {
            LevelReset.LevelRestarted -= LevelResetOnLevelRestarted;
        }
        
        void Update()
        {
            if (_playerStandDetector.IsStand)
            {
                _isRiding = true;
            }

            if (!_isRiding)
            {
                return;
            }
            
            if (_toOffset)
            {
                if (Vector3.Distance(transform.position, _initialPosition + (Vector3)_offset) > 0.05f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _initialPosition + (Vector3)_offset,
                        _speed * Time.deltaTime);
                }
                else
                {
                    _toOffset = false;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, _initialPosition) > 0.05f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _initialPosition,
                        _speed * Time.deltaTime);
                }
                else
                {
                    _isRiding = false;
                    _toOffset = true;
                }
            }
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
