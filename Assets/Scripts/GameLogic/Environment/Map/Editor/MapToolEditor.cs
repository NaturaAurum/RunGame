using Sirenix.OdinInspector.Editor;
using Stella.Data;
using Stella.Data.Enums;
using Stella.GameLogic.Environment.Floor;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace Stella.GameLogic.Environment.Map
{
    [CustomEditor(typeof(MapTool))]
    public class MapToolEditor : OdinEditor
    {
        private const string DataPathBase = "Assets/GameAsset/ScriptAsset/";
        private static readonly string MapDataPath = $"{DataPathBase}Map/";

        private MapTool mapTool = null;

        protected override void OnEnable()
        {
            base.OnEnable();
            mapTool = target as MapTool;
            SceneView.duringSceneGui -= OnSceneGui;
            SceneView.duringSceneGui += OnSceneGui;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            mapTool = null;
        }

        private void OnSceneGui(SceneView obj)
        {
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (mapTool != null)
            {
                var targetData = mapTool.Data;

                if (GUILayout.Button("Create Map Data"))
                {
                    CreateMapData();
                }

                if (GUILayout.Button("Convert"))
                {
                    Convert();
                }
                else if (GUILayout.Button("Load"))
                {
                    Load();
                }
            }
        }

        private void CreateMapData()
        {
            var id = mapTool.Id;
            var newMapData = CreateInstance<MapData>();
            newMapData.Id = id;
            newMapData.SubType = mapTool.SubType;
            newMapData.AddBlockInfo(new BlockInfo()
            {
                Id = ItemId.By(ItemType.Floor, id, 0),
                Position = Vector2.zero
            });
            AssetDatabase.CreateAsset(newMapData, $"{MapDataPath}MapData_{mapTool.SubType:D3}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            mapTool.Data = newMapData;
        }

        private void ClearTile()
        {
            var tileMap = mapTool.TargetTileMap;
            var cellBounds = tileMap.cellBounds;
            
            foreach (var pos in cellBounds.allPositionsWithin)
            {
                if (tileMap.HasTile(pos))
                {
                    tileMap.SetTile(pos, null);
                }
            }
        }

        private void Convert()
        {
            var currBlockTileData = mapTool.GetCurrBlockTileData();
            if (currBlockTileData == null)
            {
                Debug.LogError($"current block tile data not found!!");
                return;
            }
            var tileMap = mapTool.TargetTileMap;
            var cellBounds = tileMap.cellBounds;
            var data = mapTool.Data;
            data.Clear();
            foreach (var pos in cellBounds.allPositionsWithin)
            {
                if (tileMap.HasTile(pos))
                {
                    var tile = tileMap.GetTile<Tile>(pos);
                    if (tile != null)
                    {
                        var id = currBlockTileData.FindIdByValue(tile);
                        if (id.HasValue)
                        {
                            var worldPos = tileMap.CellToWorld(pos);
                            var blockInfo = new BlockInfo();
                            blockInfo.Id = id.Value;
                            blockInfo.Position = worldPos;
                            data.AddBlockInfo(blockInfo);
                        }
                    }
                }
            }
            
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
        }

        private void Load()
        {
            var currBlockTileData = mapTool.GetCurrBlockTileData();
            if (currBlockTileData == null)
            {
                Debug.LogError($"current block tile data not found!!");
                return;
            }
            ClearTile();
            var tileMap = mapTool.TargetTileMap;
            var data = mapTool.Data;
            var blockInfoList = data.BlockInfoList;
            foreach (var blockInfo in blockInfoList)
            {
                var id = blockInfo.Id;
                var tile = currBlockTileData.GetTile(id);
                var cellPos = tileMap.WorldToCell(blockInfo.Position);
                tileMap.SetTile(cellPos, tile);
            }
        }
    }
}