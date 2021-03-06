using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Stella.Data.Enums;
using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace Stella.Data
{
    [System.Serializable]
    public struct BlockResourceData : IKey<ItemId>
    {
        public ItemId Key
        {
            get => key;
            set => key = value;
        }

        [SerializeField]
        private ItemId key;

        [Required] [PreviewField(Height = 80)] public Sprite Sprite;
    }

    [Required]
    [Serializable]
    [CreateAssetMenu(menuName = "Sprites/Block")]
    public class BlockSprites : KeyTable<ItemId, BlockResourceData>
    {
        public MapType Map;
        public MapThemeType Theme;

        public Sprite GetSprite(ItemId itemId)
        {
            var data = GetValue(itemId);
            var key = data.Key;
            key.MapId = MapId.By(Theme, Map);
            data.Key = key;

            return data.Sprite;
        }
        
        #if UNITY_EDITOR
        [Button]
        private void Load()
        {
            if (Map == MapType.None && Theme == MapThemeType.None)
                return;

            Clear();
            
            var base_path = "Assets/GameAsset/Sprites/Tiles/";

            var map = "";
            if (Map != MapType.None)
            {
                map = Map == MapType.Two ? "2D/" : "2.5D/";
            }

            var theme = Theme.ToString();

            var guidPaths = AssetDatabase.FindAssets("t:Sprite", new[] {$"{base_path}{map}{theme}"});
            foreach (var guidPath in guidPaths)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guidPath);
                var spriteName = Path.GetFileNameWithoutExtension(assetPath);

                spriteName = spriteName.Replace(" ", "").Replace("&", "And").Replace("_", "");
                
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);

                int id = Theme == MapThemeType.Common
                    ? (int) CommonBlockIdUtil.ConvertEnum(spriteName)
                    : (int) TileBlockIdUtil.ConvertEnum(spriteName);
                
                Add(new BlockResourceData
                {
                    Key = ItemId.By(ItemType.Floor, MapId.By(Theme, Map), id),
                    Sprite = sprite
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