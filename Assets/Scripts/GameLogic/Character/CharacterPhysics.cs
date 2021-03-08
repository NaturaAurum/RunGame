using System;
using Stella.GameLogic.Command;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    /// <summary>
    /// Character 물리 관련 담당?
    /// </summary>
    public class CharacterPhysics : MonoBehaviour
    {
        public Vector3 Velocity => rig2D.velocity;
        
        [SerializeField] private float groundCheckDistance = 0.02f;
        
        private CharacterBase characterBase = null;
        private CharacterState currentState = null;
        private Rigidbody2D rig2D = null;
        // Collider?

        private RaycastHit2D[] groundCastResult = new RaycastHit2D[2];

        private int groundCheckLayerMask;
        private bool isGround = true;

        private void Awake()
        { 
            characterBase = GetComponent<CharacterBase>();
            characterBase.OnEnterState += OnEnterState;
            characterBase.OnExitState += OnExitState;
            
            
            rig2D = GetComponent<Rigidbody2D>();
            if (rig2D == null) // getcomponent로 못얻어오면 없는거니까 추가
                rig2D = gameObject.AddComponent<Rigidbody2D>();
            
            groundCheckLayerMask = 1 << LayerMask.NameToLayer("Block");
        }

        private void OnDestroy()
        {
            characterBase.OnEnterState -= OnEnterState;
            characterBase.OnExitState -= OnExitState;
        }

        private void OnEnterState(CharacterState state)
        {
            currentState = state;
            var vel = GetBeforeVelocity();
            state.OnEnterPhysics(ref vel);
            ApplyAfterVelocity(ref vel);
        }

        private void OnExitState(CharacterState state)
        {
            var vel = GetBeforeVelocity();
            state.OnExitPhysics(ref vel);
            ApplyAfterVelocity(ref vel);
        }

        // 나중에 뭔가 처리할 수 있지 않을까 싶어서 메서드로 분리
        
        private Vector2 GetBeforeVelocity() => rig2D.velocity;

        private void ApplyAfterVelocity(ref Vector2 velocity) => rig2D.velocity = velocity;
        
        private void FixedUpdate()
        {
            if (characterBase.IsDead || !characterBase.CanPlay) return;
            
            GroundCheck();
            var velocity = GetBeforeVelocity();
            currentState?.UpdatePhysics(ref velocity);
            ApplyAfterVelocity(ref velocity);
        }
        
        private void GroundCheck()
        {
            var groundNow = Physics2D.RaycastNonAlloc(transform.position, Vector3.down, groundCastResult,
                groundCheckDistance,
                groundCheckLayerMask) > 0;

            if (isGround != groundNow)
            {
                isGround = groundNow;

                CommandDispatcher.Dispatch(isGround ? (ICommand) new ToGroundCommand() : new ToAirCommand());
            }   
        }
    }
}