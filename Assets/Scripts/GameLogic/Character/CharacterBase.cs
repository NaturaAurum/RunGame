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

        private void Awake()
        {
            CommandDispatcher.AddListener(this);
        }

        private void OnDestroy()
        {
            CommandDispatcher.RemoveListener(this);
        }

        public void Listen(ICommand command)
        {
            if (command is ToGroundCommand)
            {
                IsGround = true;
            }            
            else if (command is ToAirCommand)
            {
                IsGround = false;
            }
        }
    }
}