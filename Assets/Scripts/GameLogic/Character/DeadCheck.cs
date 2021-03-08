using Stella.GameLogic.Command;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    public class DeadCheck : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == 10)
            {
                CommandDispatcher.Dispatch(new GameOverCommand());
            }
        }
    }
}