using Stella.GameLogic.Command;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    public abstract class JumpState : CharacterState
    {
        public JumpState(CharacterBase characterBase) : base(characterBase)
        {
        }

        public override void DoCommand(ICommand command)
        {
            if (command is ToGroundCommand)
            {
                nextState = new RunState(characterBase);
            }

            DoCommandInternal(command);

        }

        protected virtual void DoCommandInternal(ICommand command){}

        public override void UpdatePhysics(ref Vector2 velocity)
        {
            var acc = characterBase.CharacterData.AccelerationOnAir;
            var maxSpeed = characterBase.CharacterData.MaxSpeedOnAir;
            DefaultUpdatePhysics(ref velocity, Vector3.right, maxSpeed, acc);

            var velocityY = velocity.y;
            if (velocityY < 0)
            {
                nextState = new AirState(characterBase);
            }
        }
    }
}