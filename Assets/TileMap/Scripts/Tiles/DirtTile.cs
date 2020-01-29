using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DTWorlds.TileMap.Tiles
{
    public class DirtTile : DTTile
    {
        public List<Sprite> RandomTiles;

        public DirtTile()
        {
            Walkable = true;
        }

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData){
            var spriteArray = RandomTiles.ToArray();
            tileData.sprite = spriteArray[Random.Range(0,spriteArray.Length)];
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tiles/DirtTile")]
        public static void CreateDirtTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save DirtTile", "DirtTile", "asset", "Save DirtTile", "Assets");
            if (path == "")
            {
                return;
            }

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DirtTile>(), path);
        }
#endif
    }



}
