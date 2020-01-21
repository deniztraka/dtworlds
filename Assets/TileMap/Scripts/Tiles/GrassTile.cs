using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DTWorlds.TileMap.Tiles
{
    public class GrassTile : DTTile
    {
        public GrassTile()
        {
            Walkable = true;
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tiles/GrassTile")]
        public static void CreateGrassTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save GrassTile", "GrassTile", "asset", "Save GrassTile", "Assets");
            if (path == "")
            {
                return;
            }

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<GrassTile>(), path);
        }
#endif
    }



}
