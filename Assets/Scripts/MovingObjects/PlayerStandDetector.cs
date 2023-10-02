using UnityEngine;

namespace MovingObjects
{
    public class PlayerStandDetector : MonoBehaviour
    {
        private Transform _player;
        public bool IsStand => _player;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                _player = player.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.transform == _player)
            {
                _player = null;
            }
        }
    }
}
