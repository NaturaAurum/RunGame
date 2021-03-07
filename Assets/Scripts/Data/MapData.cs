using System;
using System.Collections.Generic;
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
    }
}