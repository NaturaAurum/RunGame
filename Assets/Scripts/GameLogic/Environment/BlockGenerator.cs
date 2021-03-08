using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Stella.Data;
using Stella.Data.Enums;
using Stella.GameLogic.Manager;
using UniRx;
using UnityEngine;

namespace Stella.GameLogic.Environment
{
    public class BlockGenerator : MonoBehaviour
    {
        [SerializeField, Required] private Transform blockParent = null;
        [SerializeField, Required] private GameObject blockPrefab = null;
        [SerializeField, Required] private GameObject waterPrefab = null;
        [SerializeField, Required] private GameObject obstaclePrefab = null;

        [SerializeField] private int generateCountPerFrame = 10;

        private MapData currentMapData = null;

        private List<GameObject> spawnedBlockList = new List<GameObject>();
        
        private void Awake()
        {
            GameManager.Instance.CurrentMapData.Subscribe(SetMapData).AddTo(this);
        }

        private void SetMapData(MapData mapData)
        {
            if (mapData == null)
                return;
            currentMapData = mapData;
            StartCoroutine(GenerateBlock());
        }

        private GameObject GetPrefab(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Obstacles:
                    return obstaclePrefab;
                case ItemType.Floor:
                    return blockPrefab;
                case ItemType.Water:
                    return waterPrefab;
            }

            return null;
        }

        private IEnumerator GenerateBlock()
        {
            var blocks = currentMapData.BlockInfoList;
            int count = 0;
            foreach (var blockInfo in blocks)
            {
                count++;
                var itemId = blockInfo.Id;
                var itemType = itemId.Type;
                var pos = blockInfo.Position;
                var target = GetPrefab(itemType);
                var instance = Instantiate(target, blockParent);
                instance.transform.position = pos;
                var blockComp = instance.GetComponent<Block>();
                blockComp.Id = itemId;
                blockComp.SetSprite();
                if (generateCountPerFrame <= count)
                {
                    count = 0;
                    yield return null;
                }
            }
        }
    }
}