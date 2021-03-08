using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Stella.Data
{
    [Serializable]
    public struct BlockInfo
    {
        public ItemId Id;
        public Vector2 Position;
    }
    
    public class MapData : ScriptableObject
    {
        public MapId Id;
        public int SubType;
        
        public IReadOnlyList<BlockInfo> BlockInfoList => blockInfoList;
        
        [SerializeField] private List<BlockInfo> blockInfoList;

        public void AddBlockInfo(BlockInfo blockInfo)
        {
            if (blockInfoList == null)
                blockInfoList = new List<BlockInfo>();
            blockInfoList.Add(blockInfo);
        }

        public void Clear()
        {
            blockInfoList.Clear();
        }

        [Button]
        private void Sort()
        {
            blockInfoList.Sort((v1, v2) => (int) (v1.Position.x - v2.Position.x));
        }
    }

    public class MapDataRxProp : ReactiveProperty<MapData>
    {
        public MapDataRxProp() : base(null)
        {
            
        }
    }
}