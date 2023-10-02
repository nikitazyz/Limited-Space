using System;
using Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Animator _animator;
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Fly = Animator.StringToHash("Fly");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int NoJump = Animator.StringToHash("NoJump");

        private void Awake()
        {
            _playerMovement.Jumped += () =>
            {
                _animator.SetTrigger(Jump);
            };
        }

        private void Update()
        {
            _animator.SetBool(Walk, Mathf.Abs(_playerMovement.Velocity.x) > 0.1f && Mathf.Abs(_playerMovement.ActualVelocity.x) > 0);
            _animator.SetBool(Fly, Mathf.Abs(_playerMovement.Velocity.y) > 0.1f);
            _animator.SetBool(Fall, _player.IsDead);
            if (Input.GetKeyDown(KeyCode.Space) && _player.Jumps == 0)
            {
                _animator.SetTrigger(NoJump);
            }
        }
    }
}
