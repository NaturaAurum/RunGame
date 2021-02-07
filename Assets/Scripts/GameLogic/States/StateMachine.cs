using System;
using Stella.GameLogic.Command;

namespace Stella.GameLogic.States
{
    public class StateMachine<TState> : ICommandListener where TState : class, IState<TState>
    {
        public Action<TState> OnEnterState;
        public Action<TState> OnExitState;

        public TState CurrentState { get; private set; } // set은 private 접근으로 한다.

        public StateMachine(TState initState)
        {
            CurrentState = initState;
            OnEnterState?.Invoke(CurrentState);
        }

        public void Listen(ICommand command)
        {
            CurrentState.Listen(command);
        }

        public void Update()
        {
            if (CurrentState == null) return;

            CurrentState.Update();
            if (CurrentState.NextState() != null)
            {
                ChangeState(CurrentState.NextState());
            }
        }
        
        private void ChangeState(TState nextState)
        {
            OnExitState(CurrentState);
            CurrentState = nextState;
            OnEnterState(CurrentState);
        }
    }
}