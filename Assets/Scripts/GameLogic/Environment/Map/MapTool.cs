using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Stella.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Stella.GameLogic.Environment.Map
{
    public class MapTool : MonoBehaviour
    {
        public MapData Data = null;
        public MapId Id;
        public int SubType;
        public Transform BlockParent = null;
        public GameObject BlockPrefab;

        public Tilemap TargetTileMap;
        
        [ReadOnly]
        public List<BlockTiles> TileList = new List<BlockTiles>();

        public BlockTiles GetCurrBlockTileData()
        {
            var tileData = TileList.FirstOrDefault(x => x.Map == Id.Type && x.Theme == Id.Theme);
            return tileData;
        }

    }
}
