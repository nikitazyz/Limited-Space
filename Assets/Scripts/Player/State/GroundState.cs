using Movement;
using StateMachine;
using UnityEngine;

namespace Player.State
{
    public class GroundState : IStateEnter, IMoveState
    {
        private readonly Rigidbody _rigidbody;

        public GroundState(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Enter()
        {
            
        }

        public void Move(float direction)
        {
            
        }
    }
}