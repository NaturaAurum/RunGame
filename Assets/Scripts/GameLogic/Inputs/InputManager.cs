using System;
using System.Collections;
using System.Collections.Generic;
using Stella.Data.Enums;
using Stella.GameLogic.Command;
using Stella.GameLogic.Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Stella.GameLogic.Inputs
{
    public class InputManager : MonoBehaviour
    {
        private readonly JumpCommand jumpCommand = new JumpCommand();
        
        // 거의 임시니까 나중에 필요하면 고치기

        private GameState currState = GameState.Ready;

        private void Start()
        {
            GameManager.Instance.CurrentState.Subscribe(state => currState = state).AddTo(this);
        }

        private void Update()
        {
            if (currState != GameState.Play)
                return;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CommandDispatcher.Dispatch(jumpCommand);
            }
        }
    }
}
