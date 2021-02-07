using Stella.GameLogic.Command;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    public class RunState : CharacterState
    {
        // animation 있으면 animator 가져오기
        
        public RunState(CharacterBase characterBase) : base(characterBase)
        {
            
        }

        public override void Listen(ICommand command)
        {
            // TODO : JumpCommand 들어오면 JumpState로 변환?

            if (IsJumpCommand(command))
            {
                nextState = new Jump01State(characterBase);
            }
        }

        public override void UpdatePhysics(ref Vector2 velocity)
        {
            var maxSpeed = characterBase.CharacterData.MaxSpeedOnGround;
            var acc = characterBase.CharacterData.AccelerationOnGround;
            DefaultUpdatePhysics(ref velocity, Vector2.right, maxSpeed, acc);
            base.UpdatePhysics(ref velocity);
        }
    }
}