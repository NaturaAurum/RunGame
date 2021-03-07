using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stella.Data
{
    public static class BlockDataContainer
    {
        private static IReadOnlyList<BlockSprites> blockSpriteList = null;
        
        static BlockDataContainer()
        {
            var blockSpritesArray = Resources.LoadAll<BlockSprites>("ScriptAsset/Blocks");
            blockSpriteList = blockSpritesArray.ToList();
        }

        public static Sprite GetSprite(ItemId itemId)
        {
            var mapId = itemId.MapId;

            var data = blockSpriteList.
                FirstOrDefault(CheckData(mapId));

            if (data != null)
            {
                return data.GetSprite(itemId);
            }

            return null;
        }

        private static Func<BlockSprites, bool> CheckData(MapId mapId)
        {
            return x => x.Map == mapId.Type && x.Theme == mapId.Theme;
        }
    }
}