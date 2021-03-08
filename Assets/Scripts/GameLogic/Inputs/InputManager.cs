using System;
using System.Collections;
using System.Collections.Generic;
using Stella.GameLogic.Command;
using UnityEngine;

namespace Stella.GameLogic.Inputs
{
    public class InputManager : MonoBehaviour
    {
        private readonly JumpCommand jumpCommand = new JumpCommand();
        
        // 거의 임시니까 나중에 필요하면 고치기

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CommandDispatcher.Dispatch(jumpCommand);
            }
            
            // FIXME
            // if (Input.GetKeyDown(KeyCode.S))
            // {
            //     CommandDispatcher.Dispatch(new StartCommand());
            // }
        }
    }
}
