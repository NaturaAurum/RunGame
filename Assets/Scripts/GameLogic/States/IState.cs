using Stella.GameLogic.Command;

namespace Stella.GameLogic.States
{
    public interface IState<out TState> : ICommandListener 
        where TState : class, IState<TState> 
    {
        /// <summary>
        /// 다음 State가 있는지?
        /// </summary>
        /// <returns></returns>
        TState NextState();

        /// <summary>
        /// MonoBehaviour Update랑 같은 역할 MonoBehaviour 상속받은 StateMachine에서 호출해준다거나 할듯.
        /// </summary>
        void Update();
    }
}
