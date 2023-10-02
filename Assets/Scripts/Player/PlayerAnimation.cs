using System;
using Movement;
using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _player;
        [SerializeField] private Animator _animator;
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Fly = Animator.StringToHash("Fly");
        private static readonly int Jump = Animator.StringToHash("Jump");

        private void Awake()
        {
            _player.Jumped += () =>
            {
                _animator.SetTrigger(Jump);
            };
        }

        private void Update()
        {
            _animator.SetBool(Walk, Mathf.Abs(_player.Velocity.x) > 0.1f && Mathf.Abs(_player.ActualVelocity.x) > 0);
            _animator.SetBool(Fly, Mathf.Abs(_player.Velocity.y) > 0.1f);
        }
    }
}
