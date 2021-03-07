using System;
using System.Linq;
using Sirenix.OdinInspector;
using Stella.Data.Enums;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif


namespace Stella.Data
{

    [Serializable]
    public struct BlockTileResource : IKey<ItemId>
    {
        public ItemId Key
        {
            get => key;
            set => key = value;
        }
        [SerializeField] private ItemId key;

        [Required] [PreviewField(Height = 80)] public Tile Tile;
    }
    
    [Required]
    [Serializable]
    [CreateAssetMenu(menuName = "Block/Tiles")]
    public class BlockTiles : KeyTable<ItemId, BlockTileResource>
    {
        public MapType Map;
        public MapThemeType Theme;

        public ItemType Type;
        
        public Tile GetTile(ItemId itemId)
        {
            var data = GetValue(itemId);
            var key = data.Key;
            key.MapId = MapId.By(Theme, Map);
            data.Key = key;

            return data.Tile;
        }

        public ItemId? FindIdByValue(Tile tile)
        {
            foreach (var value in Values)
            {
                if (value.Tile == tile)
                    return value.Key;
            }

            return null;
        }
        
#if UNITY_EDITOR
        [Button]
        private void Load()
        {
            if (Map == MapType.None && Theme == MapThemeType.None)
                return;

            Clear();
            
            var base_path = "Assets/GameAsset/TileMap/Palette/";
            var theme = Theme.ToString();

            var two_five = Map == MapType.Two_Five ? "25d" : "";

            var guidPaths = AssetDatabase.FindAssets("t:Tile", new[] {$"{base_path}{theme}{two_five}"});
            foreach (var guidPath in guidPaths)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guidPath);
                var spriteName = Path.GetFileNameWithoutExtension(assetPath);

                spriteName = spriteName.Replace(" ", "").Replace("&", "And").Replace("_", "");
                
                var tile = AssetDatabase.LoadAssetAtPath<Tile>(assetPath);

                int id = Theme == MapThemeType.Common
                    ? (int) CommonBlockIdUtil.ConvertEnum(spriteName)
                    : (int) TileBlockIdUtil.ConvertEnum(spriteName);
                
                Add(new BlockTileResource()
                {
                    Key = ItemId.By(ItemType.Floor, MapId.By(Theme, Map), id),
                    Tile = tile
                });
            }
        }

        [Button]
        private void Sort()
        {
            values = values.OrderBy(x => x.Key.Id).ToList();
        }
#endif
    }
}