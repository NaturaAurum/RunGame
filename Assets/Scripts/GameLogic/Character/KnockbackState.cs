using Stella.GameLogic.Command;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    public class KnockbackState : CharacterState
    {
        public override CharacterStateType Type => CharacterStateType.Knockback;
        
        private readonly Vector2 knockBack = Vector2.zero;
        
        public KnockbackState(CharacterBase characterBase) : base(characterBase)
        {
            knockBack = characterBase.CharacterData.HitKnockback;
        }

       
        public override void DoCommand(ICommand command)
        {
            if (command is ToGroundCommand)
            {
                nextState = new RunState(characterBase);
            }
        }

        public override void OnEnterPhysics(ref Vector2 velocity)
        {
            velocity = knockBack;
        }

        public override void UpdatePhysics(ref Vector2 velocity)
        {
            UpdateKnockback(ref velocity);
        }
    }
}