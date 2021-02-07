using Stella.GameLogic.Command;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    public class JumpState : CharacterState
    {
        public JumpState(CharacterBase characterBase) : base(characterBase)
        {
        }

        public override void Listen(ICommand command)
        {
            if (command is ToGroundCommand)
            {
                nextState = new RunState(characterBase);
            }
            DoCommand(command);
        }
        
        protected virtual void DoCommand(ICommand command) {}

        public override void UpdatePhysics(ref Vector2 velocity)
        {
            var acc = characterBase.CharacterData.AccelerationOnAir;
            var maxSpeed = characterBase.CharacterData.MaxSpeedOnAir;
            DefaultUpdatePhysics(ref velocity, Vector3.right, maxSpeed, acc);
        }
    }
}