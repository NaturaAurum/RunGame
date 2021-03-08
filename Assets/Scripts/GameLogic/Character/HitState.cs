using UnityEngine;
using Stella.GameLogic.Command;

namespace Stella.GameLogic.Character
{
    // TODO : Invincible
    public class HitState : CharacterState
    {
        public override CharacterStateType Type => CharacterStateType.Hit;

        private CharacterBase characterBase = null;
        
        public HitState(CharacterBase characterBase) : base(characterBase)
        {
            this.characterBase = characterBase;
            hitstopFrame = characterBase.CharacterData.HitstopFrame;
        }


        public override void DoCommand(ICommand command)
        {
            return; // do nothing
        }

        public override void Update()
        {
            base.Update();
            if (hitstopFrame <= 0)
            {
                nextState = new KnockbackState(characterBase);
            }
        }

        public override void UpdatePhysics(ref Vector2 velocity)
        {
            velocity = Vector2.zero;
        }
    }
}