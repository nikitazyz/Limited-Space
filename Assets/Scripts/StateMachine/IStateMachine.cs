using System;

namespace StateMachine
{
    public interface IStateMachine
    {
        public event Action<Type> StateChanged; 
        public Type CurrentStateType { get; }
        public void AddState<T>() where T : IState, new();
        public void AddState<T>(T state) where T : IState;
        public void ChangeState<T>() where T : IState;
    }
}