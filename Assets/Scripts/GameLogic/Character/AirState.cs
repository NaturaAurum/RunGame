using Stella.GameLogic.Command;

namespace Stella.GameLogic.Character
{
    /// <summary>
    /// 공중에 있을 때
    /// </summary>
    public class AirState : CharacterState
    {
        public AirState(CharacterBase characterBase) : base(characterBase)
        {
        }

        public override void Listen(ICommand command)
        {
            
        }
    }
}