using UnityEngine;
using Stella.GameLogic.Command;

namespace Stella.GameLogic.Character
{
    // TODO : Invincible
    
    public class HitState : CharacterState
    {
        private readonly int hitStopFrame = 0;
        private readonly Vector2 knockBack = Vector2.zero;
        private int frame = 0;

        private CharacterBase characterBase = null;
        
        public HitState(CharacterBase characterBase) : base(characterBase)
        {
            frame = 0;
            this.characterBase = characterBase;
        }

        public HitState(CharacterBase characterBase, int hitStopFrame, Vector2 knockBack) : this(characterBase)
        {
            this.hitStopFrame = hitStopFrame;
            this.knockBack = knockBack;
        }

        public override void Listen(ICommand command)
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
            // 필요하다면 구현하고 아니면 삭제
        }
    }
}