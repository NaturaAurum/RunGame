using Stella.GameLogic.Command;

namespace Stella.GameLogic.Character
{
    public class IdleState : CharacterState
    {
        public IdleState(CharacterBase characterBase) : base(characterBase)
        {
            
        }

        public override void Listen(ICommand command)
        {
            if (command is StartCommand)
            {
                nextState = new RunState(characterBase);
            }
        }
    }
}