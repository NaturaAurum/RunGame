using System;
using Stella.GameLogic.Character.Data;
using Stella.GameLogic.Command;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    /// <summary>
    /// 캐릭터 관련 Base Component
    /// </summary>
    public class CharacterBase : MonoBehaviour, ICommandListener
    {
        public Action<CharacterState> OnEnterState;
        public Action<CharacterState> OnExitState;

        public CharacterData CharacterData;

        public bool CanJump => RemainJumpCount > 0;
        
        /// <summary>
        /// 남은 JumpCount
        /// </summary>
        public int RemainJumpCount { get; private set; }
        
        /// <summary>
        /// 죽었는지?
        /// </summary>
        public bool IsDead { get; private set; }
        
        public bool IsGround { get; private set; }

        public float Gravity => CharacterData.GravityOnAir;

        public bool Invincible { get; set; }
        
        public Transform CamTarget { get; set; }

        private void Awake()
        {
            CommandDispatcher.AddListener(this);
            OnEnterState += InternalOnEnterState;
            OnExitState += InternalOnExitState;

            RemainJumpCount = CharacterData.MaxJumpCount;
        }

        private void OnDestroy()
        {
            CommandDispatcher.RemoveListener(this);
            OnEnterState -= InternalOnEnterState;
            OnExitState -= InternalOnExitState;
        }

        private void InternalOnExitState(CharacterState state)
        {
            if (state is JumpState)
            {
                RemainJumpCount--;
            }
        }

        private void InternalOnEnterState(CharacterState state)
        {
            
        }

        public void Listen(ICommand command)
        {
            if (command is ToGroundCommand)
            {
                IsGround = true;
                RemainJumpCount = CharacterData.MaxJumpCount;
            }            
            else if (command is ToAirCommand)
            {
                IsGround = false;
            }
        }
    }
}