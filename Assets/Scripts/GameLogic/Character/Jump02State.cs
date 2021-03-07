using Stella.GameLogic.Command;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    public class Jump02State : JumpState
    {
        public override CharacterStateType Type => CharacterStateType.Jump02;
        public Jump02State(CharacterBase characterBase) : base(characterBase)
        {
        }

        public override void OnEnterPhysics(ref Vector2 velocity)
        {
            var velNow = velocity;
            velNow.y = characterBase.CharacterData.Jump02Speed;
            velocity = velNow;
        }
    }
}