using Stella.GameLogic.Command;
using Stella.GameLogic.States;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    public enum CharacterStateType
    {
        Idle,
        Air,
        Hit,
        Knockback,
        Run,
        Jump01,
        Jump02,
    }

    /// <summary>
    /// 캐릭터 관련된 State 들이 상속받는 클래스
    /// </summary>
    public abstract class CharacterState : IState<CharacterState>
    {
        public abstract CharacterStateType Type { get; }

        protected CharacterBase characterBase { get; }

        protected CharacterState nextState = null;

        protected int hitstopFrame = 0;
        protected bool hitstopNow => hitstopFrame > 0;

        protected CharacterState(CharacterBase characterBase)
        {
            this.characterBase = characterBase;
        }

        public void Listen(ICommand command)
        {
            // 여러가지 캐릭터 공통 command 처리

            if (command is CharacterHitCommand)
            {
                nextState = new HitState(characterBase);
            }

            if (command is InitCommand)
            {
                nextState = new IdleState(characterBase);
            }

            DoCommand(command);
        }

        public abstract void DoCommand(ICommand command);

        public virtual CharacterState NextState() => nextState;

        public virtual void Update()
        {
            if (hitstopNow)
            {
                hitstopFrame--;
            }
        }

        protected bool IsJumpCommand(ICommand command) => command is JumpCommand && characterBase.CanJump;

        public virtual void UpdatePhysics(ref Vector2 velocity)
        {
        }

        protected void UpdateKnockback(ref Vector2 velocity)
        {
            if (!characterBase.IsGround)
            {
                float y = velocity.y;
                var gravity = characterBase.Gravity;
                y -= gravity * Time.fixedDeltaTime;
                velocity.y = y;
            }
        }

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

        public virtual void OnEnterPhysics(ref Vector2 velocity)
        {
        }

        public virtual void OnExitPhysics(ref Vector2 velocity)
        {
        }
    }
}