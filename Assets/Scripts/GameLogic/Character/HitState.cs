using UnityEngine;
using Stella.GameLogic.Command;

namespace Stella.GameLogic.Character
{
    // TODO : Invincible
    public class HitState : CharacterState
    {
        public override CharacterStateType Type => CharacterStateType.Hit;
        
        private readonly int hitStopFrame = 0;
        private int frame = 0;

        private CharacterBase characterBase = null;
        
        public HitState(CharacterBase characterBase) : base(characterBase)
        {
            frame = 0;
            this.characterBase = characterBase;
            hitstopFrame = characterBase.CharacterData.HitstopFrame;
        }


        public override void DoCommand(ICommand command)
        {
            return; // do nothing
        }

        public override void Update()
        {
            frame++;
            if (frame >= hitStopFrame)
            {
                nextState = new RunState(characterBase);
            }
        }

        public override void OnEnterPhysics(ref Vector2 velocity)
        {
            // 넉벡
        }
    }
}