using Stella.Data.Enums;
using Stella.GameLogic.Manager;
using UniRx;
using UnityEngine;

namespace Stella.GameLogic.Environment
{
    public class SpikeGenerator : MonoBehaviour
    {
        private GameObject spikePrefab = null;

        [SerializeField] private float generateTime = 3f;
        private float timer = 0f;

        private GameState currState = GameState.Ready;
        
        private void Awake()
        {
            GameManager.Instance.CurrentState.Subscribe(OnGameStateChanged).AddTo(this);

            spikePrefab = Resources.Load<GameObject>($"Prefabs/Spike");
        }

        private void Update()
        {
            if (currState == GameState.Play)
            {
                timer += Time.deltaTime;
                if (timer >= generateTime)
                {
                    Generate();
                    timer = 0f;
                }
            }
        }

        private void Generate()
        {
            Instantiate(spikePrefab, transform.position + Vector3.left * 4f, Quaternion.identity);
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Ready:
                case GameState.Over:
                    break;
                case GameState.Play:
                    break;
            }
        }
    }
}