using System;
using System.Collections;
using System.Collections.Generic;
using Stella.Data;
using Stella.Data.Enums;
using Stella.GameLogic.Command;
using UniRx;
using UnityEngine;

namespace Stella.GameLogic.Manager
{
    [DefaultExecutionOrder(-1000)]
    public class GameManager : MonoBehaviour, ICommandListener
    {
        public static GameManager Instance { get; private set; }

        public IReadOnlyReactiveProperty<GameState> CurrentState => currentState;
        public IReadOnlyReactiveProperty<MapData> CurrentMapData => currentMapData;

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


        private GameStateRxProp currentState = new GameStateRxProp(GameState.Ready);

        private MapDataRxProp currentMapData = new MapDataRxProp();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            CommandDispatcher.AddListener(this);

            CurrentMapData.Subscribe(MapDataSetUpDone).AddTo(this);
        }

        private void MapDataSetUpDone(MapData mapData)
        {
            if (mapData != null)
            {
                var playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
                var playerInstance = Instantiate(playerPrefab);
                playerInstance.transform.position = StartPos;
            }
        }

        private void OnDestroy()
        {
            CommandDispatcher.RemoveListener(this);
        }

        public void Listen(ICommand command)
        {
            
        }

        public void SetMapData(MapData mapData)
        {
            currentMapData.Value = mapData;
        }
    }
}
