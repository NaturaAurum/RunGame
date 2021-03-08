using Stella.GameLogic.Command;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    /// <summary>
    /// 공중에 있을 때
    /// </summary>
    public class AirState : CharacterState
    {
        public override CharacterStateType Type => CharacterStateType.Air;
        
        public AirState(CharacterBase characterBase) : base(characterBase)
        {
        }
        
        public override void DoCommand(ICommand command)
        {
            if (command is ToGroundCommand)
            {
                nextState = new RunState(characterBase);
            }
            
            if (IsJumpCommand(command))
            {
                nextState = new Jump02State(characterBase);
            }
        }

        public override void UpdatePhysics(ref Vector2 velocity)
        {
            if (!hitstopNow)
            {
                var maxSpeedOnAir = characterBase.CharacterData.MaxSpeedOnAir;
                var acc = characterBase.CharacterData.AccelerationOnAir;
                DefaultUpdatePhysics(ref velocity, Vector3.right, maxSpeedOnAir, acc);
            }
            base.UpdatePhysics(ref velocity);
        }
    }
}