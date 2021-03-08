using System;
using Stella.Utils;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    /// <summary>
    /// Character Animator 관리
    /// Character Model을 대체한다
    /// </summary>
    public class CharacterAnimator : MonoBehaviour
    {
        private const string DEFAULT_CHARACTER = "Character_001";
        
        public GameObject Model { get; private set; }

        private CharacterBase character = null;

        private Animator animator = null;

        private float RunSpeed => character.CharacterData.MaxSpeedOnGround / 5f;

        private void Awake()
        {
            character = GetComponent<CharacterBase>();
            var model = Resources.Load<GameObject>($"Prefabs/{DEFAULT_CHARACTER}");
            Debug.Assert(model != null);

            var instance = Instantiate(model, transform);
            var modelTf = instance.transform;

            var characterData = character.CharacterData;

            modelTf.localPosition = characterData.LocalPosition;
            modelTf.localRotation = Quaternion.Euler(characterData.LocalRotation);
            modelTf.localScale = characterData.LocalScale;

            animator = instance.GetComponent<Animator>();

            character.OnEnterState += OnEnterState;
            character.OnExitState += OnExitState;

            var camTarget = instance.FindDeep<Transform>("CamTarget");
            character.CamTarget = (camTarget == null) ? transform : camTarget;

            Model = instance;

            animator.speed = RunSpeed;
        }

        private void OnDestroy()
        {
            character.OnEnterState -= OnEnterState;
            character.OnExitState -= OnExitState;
        }

        private void OnExitState(CharacterState state)
        {
            if (state is HitState)
                animator.speed = RunSpeed;
        }

        private void OnEnterState(CharacterState state)
        {
            if (state is HitState)
                animator.speed = 0;
            else
                animator.Play(state.Type.ToString(), 0);
        }
    }
}