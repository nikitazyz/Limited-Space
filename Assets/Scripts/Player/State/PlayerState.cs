using Movement;
using StateMachine;

namespace Player.State
{
    public abstract class PlayerState : IState
    {
        public readonly PlayerMovement PlayerMovement;

        protected PlayerState(PlayerMovement playerMovement)
        {
            PlayerMovement = playerMovement;
        }
    }
}