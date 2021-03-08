using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Stella.Data;
using Stella.Data.Enums;
using Stella.GameLogic.Manager;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;

#endif

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

        public GameObject GridObject = null;

        [ReadOnly] public List<BlockTiles> TileList = new List<BlockTiles>();

        [SerializeField, Required] private Button startButton = null;
        [SerializeField, Required] private Button resetButton = null;


        private void Awake()
        {
            GridObject.SetActive(false);

            startButton.OnClickAsObservable().Subscribe(_ => _Start()).AddTo(this);
            resetButton.OnClickAsObservable().Subscribe(_ => _Reset()).AddTo(this);
        }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            GameManager.Instance.SetMapData(Data);
        }

        public void _Reset()
        {
            GameManager.Instance.SetState(GameState.Ready);
        }

        public void _Start()
        {
            GameManager.Instance.SetState(GameState.Play);
        }

        [Button]
        private void LoadBlockTilesData()
        {
#if UNITY_EDITOR
            TileList.Clear();
            var guids = AssetDatabase.FindAssets($"t:{nameof(BlockTiles)}", 
                new[] {"Assets/GameAsset/TileMap/Data"});
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<BlockTiles>(assetPath);
                if (asset != null)
                {
                    TileList.Add(asset);
                }
            }
#endif
        }

        private BlockTiles GetBlockTiles(ItemType itemType, MapType mapType, MapThemeType theme)
        {
            var tileData = TileList.FirstOrDefault(x => x.Map == mapType && x.Theme == theme && x.Type == itemType);
            return tileData;
        }
        
        public BlockTiles GetCurrBlockTileData()
        {
            return GetBlockTiles(ItemType.Floor, Id.Type, Id.Theme);
        }

        public BlockTiles GetCommonBlockTileData()
        {
            return GetBlockTiles(ItemType.Floor, MapType.None, MapThemeType.Common);
        }

        public BlockTiles GetObstacleBlockTileData()
        {
            return GetBlockTiles(ItemType.Obstacles, MapType.None, MapThemeType.None);
        }

        public ItemId? FindIdByValue(Tile tile)
        {
            var id = GetCurrBlockTileData()?.FindIdByValue(tile);
            if (!id.HasValue)
            {
                id = GetCommonBlockTileData()?.FindIdByValue(tile);
            }

            if (!id.HasValue)
            {
                id = GetObstacleBlockTileData()?.FindIdByValue(tile);
            }
            
            Debug.Assert(id.HasValue, $"{tile.name} id's not found");

            return id;
        }

        public Tile FindTileById(ItemId id)
        {
            var value = GetCurrBlockTileData()?.GetValue(id).Tile;
            if (value == null)
            {
                value = GetCommonBlockTileData()?.GetValue(id).Tile;
            }

            if (value == null)
            {
                value = GetObstacleBlockTileData()?.GetValue(id).Tile;
            }

            Debug.Assert(value != null, $"{id} id's not found");
            
            return value;
        }
    }
}