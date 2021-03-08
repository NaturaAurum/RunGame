namespace Stella.GameLogic.Command
{
    public interface ICommand
    {

    }

    public class ToGroundCommand : ICommand
    {
        
    }

    public class ToAirCommand : ICommand
    {
        
    }
    
    /// <summary>
    /// 게임시작 커맨드?
    /// </summary>
    public class StartCommand : ICommand
    {
        
    }

    public class JumpCommand : ICommand
    {
        // TODO : 필요한 멤버 있으면 구현하기   
    }

    public class CharacterHitCommand : ICommand
    {
        
    }
}
