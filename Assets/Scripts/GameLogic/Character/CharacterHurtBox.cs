using System;
using Stella.GameLogic.Command;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    /// <summary>
    /// Character 피격처리 담당
    /// </summary>
    public class CharacterHurtBox : MonoBehaviour
    {
        private CharacterBase character = null;
        private CharacterPhysics physics = null;

        private CapsuleCollider2D collider = null;

        private int checkLayer = 0;

        private void Awake()
        {
            character = GetComponent<CharacterBase>();
            physics = GetComponent<CharacterPhysics>();
            checkLayer = LayerMask.NameToLayer("Obstacles");
        }

        private void Start()
        {
            if (collider == null)
            {
                collider = GetComponentInChildren<CapsuleCollider2D>();
            }
        }

        private void FixedUpdate()
        {
            if (collider == null || character.Invincible)
                return;
            var velocity = physics.Velocity;
            var next = velocity * Time.fixedDeltaTime;
            var raycastHit2D = Physics2D.CapsuleCast(transform.position, collider.size, collider.direction, 0, next,
                1 << checkLayer);
            if (raycastHit2D != null && raycastHit2D.transform != null)
            {
                CommandDispatcher.Dispatch(new CharacterHitCommand());
            }
        }
    }
}