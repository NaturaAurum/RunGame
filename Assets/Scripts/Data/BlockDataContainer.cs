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
            var data = blockSpriteList.
                FirstOrDefault(CheckData(itemId));

            if (data != null)
            {
                return data.GetSprite(itemId);
            }

            return null;
        }

        private static Func<BlockSprites, bool> CheckData(ItemId id)
        {
            return x => x.Type == id.Type && x.Map == id.MapId.Type && x.Theme == id.MapId.Theme;
        }
    }
}