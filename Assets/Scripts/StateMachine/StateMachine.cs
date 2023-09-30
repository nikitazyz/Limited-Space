using System;
using System.Collections.Generic;
using System.Linq;

namespace StateMachine
{
    public class StateMachineBase : IStateMachine
    {
        private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();

        public event Action<Type> StateChanged;
        protected IState CurrentState => _states[CurrentStateType];
        public Type CurrentStateType { get; private set; }
        public void AddState<T>() where T : IState, new()
        {
            _states.Add(typeof(T), new T());
        }

        public void AddState<T>(T state) where T : IState
        {
            _states.Add(typeof(T), state);
        }

        public void ChangeState<T>() where T : IState
        {
            if (CurrentState is IStateExit exit)
            {
                exit.Exit();
            }
            CurrentStateType = typeof(T);
            if (CurrentState is IStateEnter enter)
            {
                enter.Enter();
            }
            
            StateChanged?.Invoke(typeof(T));
        }
    }
}
