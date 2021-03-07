using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using UnityEditor;

namespace Stella.GameLogic.Environment.Map
{
    [CustomEditor(typeof(MapTool))]
    public class MapToolEditor : OdinEditor
    {
        private MapTool mapTool = null;

        protected override void OnEnable()
        {
            base.OnEnable();
            mapTool = target as MapTool;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            mapTool = null;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (mapTool != null)
            {
                var targetData = mapTool.Data;

                if (targetData == null)
                {
                    if (GUILayout.Button("Create Map Data"))
                    {
                        
                    }
                }
            }
        }
    }
}
