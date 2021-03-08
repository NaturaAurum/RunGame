using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stella.Data
{
    public static class MapDataContainer
    {
        public static IReadOnlyList<MapData> MapDataList => mapDataList;
        private static IReadOnlyList<MapData> mapDataList = null;
        
        static MapDataContainer()
        {
            var mapDataArr = Resources.LoadAll<MapData>("ScriptAsset/Map");
            mapDataList = mapDataArr.ToList();
        }

        public static MapData GetMapData(int subType)
        {
            var data = mapDataList.FirstOrDefault(CheckData(subType));

            return data;
        }

        private static Func<MapData, bool> CheckData(int subType)
        {
            return x => x.SubType == subType;
        }
    }
}