using UnityEngine;

namespace Stella.GameLogic.Environment
{
    public class Spike : MonoBehaviour
    {
        [SerializeField] private float Speed = 10;
        private void Update()
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
        }
    }
}