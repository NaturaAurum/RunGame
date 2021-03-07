using System;
using Stella.Data.Enums;

namespace Stella.Data
{
    [Serializable]
    public struct ItemId
    {
        public ItemType Type;
        public MapId MapId;
        public int Id;

        public static ItemId ByType(
            ItemType type
        )
        {
            return By(type, MapId.None, 0);
        }

        public static ItemId ByMapId(
            MapId mapId
        )
        {
            return By(ItemType.None, mapId, 0);
        }
        
        public static ItemId By(
            ItemType itemType,
            MapId mapId,
            int id
        )
        {
            var itemId = new ItemId();
            itemId.Type = itemType;
            itemId.MapId = mapId;
            itemId.Id = id;
            return itemId;
        }

        public override string ToString()
        {
            return $"{Type}.{MapId}.{Id}";
        }

        public static bool operator ==(ItemId i1, ItemId i2)
        {
            var typeSame = i1.Type == i2.Type;
            var mapSame = i1.MapId == i2.MapId;
            var idSame = i1.Id == i2.Id;
            return typeSame && mapSame && idSame;
        }

        public static bool operator !=(ItemId i1, ItemId i2) => !(i1 == i2);
    }

    [Serializable]
    public struct MapId
    {
        public static MapId None => ByType(MapType.None);
        
        public MapThemeType Theme;
        public MapType Type;

        public static MapId ByType(
            MapType type
        )
        {
            return By(MapThemeType.None, type);
        }
        
        public static MapId ByTheme(
            MapThemeType theme
        )
        {
            return By(theme, MapType.None);
        }
        
        public static MapId By(
            MapThemeType theme,
            MapType map
        )
        {
            var mapId = new MapId();
            mapId.Theme = theme;
            mapId.Type = map;

            return mapId;
        }

        public override string ToString()
        {
            return $"{Type}.{Theme}";
        }

        public static bool operator ==(MapId x1, MapId x2)
        {
            var themeSame = x1.Theme == x2.Theme;
            var typeSame = x1.Type == x2.Type;
            return themeSame && typeSame;
        }

        public static bool operator !=(MapId x1, MapId x2) => !(x1 == x2);
    }
}
