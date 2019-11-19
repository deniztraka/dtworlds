using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DTWorlds.TileMap.Tiles
{
    public class DTTile : Tile
    {
        public string SomeCustomProperty;

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tiles/DTTile")]
        public static void CreatePrisonTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save DTTile", "DTTile", "asset", "Save DT tile", "Assets");
            if (path == "")
            {
                return;
            }

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DTTile>(), path);
        }
#endif
    }



}
