using System;
using System.Collections;
using System.Collections.Generic;
using Stella.Data;
using Stella.Data.Enums;
using Stella.GameLogic.Character;
using Stella.GameLogic.Command;
using UniRx;
using UnityEngine;

namespace Stella.GameLogic.Manager
{
    public static class MapDeliver
    {
        public static int MapID;
    }
    
    [DefaultExecutionOrder(-1000)]
    public class GameManager : MonoBehaviour, ICommandListener
    {
        public static GameManager Instance { get; private set; }

        public IReadOnlyReactiveProperty<GameState> CurrentState => currentState;
        public IReadOnlyReactiveProperty<MapData> CurrentMapData => currentMapData;
        public IReadOnlyReactiveProperty<float> PlayTime => playTime;

        public Vector2 StartPos
        {
            get
            {
                if (currentMapData.Value == null)
                    return Vector2.zero;

                var mapData = currentMapData.Value;
                var blockData = mapData.BlockInfoList;
                if (blockData.Count == 0)
                {
                    return Vector2.zero;
                }

                // 처음 block이 start
                // 근데 block pos 를 그대로 쓰면 block이랑 같아지니까
                // 2.56만큼 높여준다.
                // block 사이즈 기준이 5.12라서 / 2 한 값을 올려줌.
                // 급하니까 일단 하드코딩
                return blockData[0].Position + Vector2.up * 2.56f;
            }
        }

        public Vector3 EndPos
        {
            get
            {
                if (currentMapData.Value == null)
                    return Vector2.zero;

                var mapData = currentMapData.Value;
                var blockData = mapData.BlockInfoList;
                if (blockData.Count == 0)
                {
                    return Vector2.zero;
                }
                return blockData[blockData.Count - 1].Position + Vector2.up * 2.56f;
            }
        }


        private GameStateRxProp currentState = new GameStateRxProp(GameState.Ready);

        private MapDataRxProp currentMapData = new MapDataRxProp();

        private FloatReactiveProperty playTime = new FloatReactiveProperty();

        public CharacterBase CurrentPlayer => currentPlayer;

        private CharacterBase currentPlayer = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            CommandDispatcher.AddListener(this);

            

            currentMapData.Subscribe(MapDataSetUpDone).AddTo(this);
            currentState.Subscribe(OnGameStateChanged).AddTo(this);
        }

        private void Start()
        {
            SetMapData(MapDataContainer.GetMapData(MapDeliver.MapID));
        }

        private void MapDataSetUpDone(MapData mapData)
        {
            if (mapData != null)
            {
                var playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
                var playerInstance = Instantiate(playerPrefab);
                playerInstance.transform.position = StartPos;
                currentPlayer = playerInstance.GetComponent<CharacterBase>();
                CameraController.Instance.SetCharacter(currentPlayer);
            }
        }

        private void Update()
        {
            if (currentState.Value == GameState.Play)
            {
                playTime.Value += Time.deltaTime;
            }
            
            EndCheck();
        }

        private void EndCheck()
        {
            var playerPos = currentPlayer.transform.position;
            var dir = EndPos - playerPos;
            if (dir.sqrMagnitude <= 0.01f)
            {
                SetState(GameState.Clear);
            }
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Play:
                    CommandDispatcher.Dispatch(new StartCommand());
                    break;
                case GameState.Ready:
                    CommandDispatcher.Dispatch(new InitCommand());
                    if (currentPlayer != null)
                    {
                        currentPlayer.transform.position = StartPos;
                    }

                    break;
            }
        }

        private void OnDestroy()
        {
            CommandDispatcher.RemoveListener(this);
        }

        public void Listen(ICommand command)
        {
            if (command is GameOverCommand)
            {
                SetState(GameState.Over);
            }
        }

        public void SetMapData(MapData mapData)
        {
            currentMapData.Value = mapData;
        }

        public void SetState(GameState state)
        {
            currentState.Value = state;
        }
    }
}
