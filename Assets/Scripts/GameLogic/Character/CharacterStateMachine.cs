using System.Collections;
using System.Collections.Generic;
using Stella.GameLogic.Command;
using Stella.GameLogic.States;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    [RequireComponent(typeof(CharacterBase))]
    public class CharacterStateMachine : MonoBehaviour, ICommandListener
    {
        private CharacterBase characterBase = null;
        private StateMachine<CharacterState> stateMachine = null;

        private void Awake()
        {
            characterBase = GetComponent<CharacterBase>();
            // state machine init
            stateMachine = new StateMachine<CharacterState>(new IdleState(characterBase));

            stateMachine.OnEnterState = (state) => characterBase.OnEnterState?.Invoke(state);
            stateMachine.OnExitState = (state) => characterBase.OnExitState?.Invoke(state);

            CommandDispatcher.AddListener(this);
        }

        private void OnDestroy()
        {
            CommandDispatcher.RemoveListener(this);
        }

        public void Listen(ICommand command)
        {
            // TODO : 
            stateMachine.Listen(command);
        }

        private void Update()
        {
            stateMachine.Update();
        }
    }
}
