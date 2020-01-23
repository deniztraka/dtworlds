using DTWorlds.Mobiles;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DTWorlds.TileMap.Tiles
{
    public class RockTile : DTTile
    {
        public RockTile()
        {
            Walkable = false;
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tiles/RockTile")]
        public static void CreateRockTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save RockTile", "RockTile", "asset", "Save RockTile", "Assets");
            if (path == "")
            {
                return;
            }

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RockTile>(), path);
        }
#endif
    }



}
