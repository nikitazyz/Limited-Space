using System;
using LevelSystem;
using Movement;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public static event Action Jumped;
        
        [SerializeField] private PlayerMovement _playerMovement;

        private Vector3 _initialPosition;
        private bool _isDead;
        public int Jumps { get; set; }

        public bool IsDead
        {
            get => _isDead;
            set
            {
                _isDead = value;
                _playerMovement.Move(0);
            }
        }

        private void Start()
        {
            _initialPosition = transform.position;
            LevelReset.LevelRestarted += () =>
            {
                Jumps = LevelSettings.Settings.InitialJumps;
                transform.position = _initialPosition;
                IsDead = false;
                _playerMovement.CanJump = true;
            };
            _playerMovement.Jumped += () =>
            {
                Jumps--;
                Debug.Log(Jumps);
                if (Jumps == 0)
                {
                    _playerMovement.CanJump = false;
                }
                Jumped?.Invoke();
            };
            Jumps = LevelSettings.Settings.InitialJumps;
            Debug.Log(Jumps);
        }

        private void Update()
        {
            if (IsDead)
            {
                _playerMovement.Move(0);
                return;
            }
            _playerMovement.Move(InputManager.GetMovement());
            _playerMovement.IsFly = InputManager.GetFly();

            if (InputManager.GetJump())
            {
                _playerMovement.Jump();
            }
        }
    }
}