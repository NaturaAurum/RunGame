using Stella.GameLogic.Command;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    public class Jump01State : JumpState
    {
        public override CharacterStateType Type => CharacterStateType.Jump01;
        public Jump01State(CharacterBase characterBase) : base(characterBase)
        {
        }

        protected override void DoCommandInternal(ICommand command)
        {
            if (IsJumpCommand(command))
            {
                nextState = new Jump02State(characterBase);
            }
        }

        public override void OnEnterPhysics(ref Vector2 velocity)
        {
            var velNow = velocity;
            velNow = Vector2.right * characterBase.CharacterData.MaxSpeedOnAir;
            velNow.y = characterBase.CharacterData.Jump01Speed;
            velocity = velNow;
        }
    }
}