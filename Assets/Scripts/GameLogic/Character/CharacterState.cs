using Stella.GameLogic.Command;
using Stella.GameLogic.States;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    public enum CharacterStateType
    {
        Idle,
        Air,
        Run,
        Jump01,
        Jump02,
    }
    
    /// <summary>
    /// 캐릭터 관련된 State 들이 상속받는 클래스
    /// </summary>
    public abstract class CharacterState : IState<CharacterState>
    {
        protected CharacterBase characterBase { get; }

        protected CharacterState nextState = null;
        
        protected CharacterState(CharacterBase characterBase)
        {
            this.characterBase = characterBase;
        }

        public abstract void Listen(ICommand command);

        public virtual CharacterState NextState() => nextState;

        public virtual void Update() {}

        protected bool IsJumpCommand(ICommand command) => command is JumpCommand;

        public virtual void UpdatePhysics(ref Vector2 velocity) {}

        protected void DefaultUpdatePhysics(ref Vector2 velocity, Vector2 direction, float targetX, float accX)
        {
            // Vector2 targetVel = direction.normalized * targetX;

            var currX = velocity.x;

            var diff = targetX - currX;

            float changeNow = accX * Time.fixedDeltaTime;

            if (diff > changeNow)
            {
                var add = diff * changeNow;
                velocity.x += add;
            }
            else
            {
                velocity.x = targetX;
            }

            if (!characterBase.IsGround)
            {
                float y = velocity.y;

                float gravity = characterBase.Gravity;

                y -= gravity * Time.fixedDeltaTime;

                velocity.y = y;
            }
        }

        public virtual void OnEnterPhysics(ref Vector2 velocity) {}
        public virtual void OnExitPhysics(ref Vector2 velocity) {}
    }
}