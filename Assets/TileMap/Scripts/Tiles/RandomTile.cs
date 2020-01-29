using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DTWorlds.TileMap.Tiles
{
    public class RandomTile : DTTile
    {
        public List<Sprite> RandomTiles;

        public RandomTile()
        {
            Walkable = true;
        }

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData){
            var spriteArray = RandomTiles.ToArray();
            tileData.sprite = spriteArray[Random.Range(0,spriteArray.Length)];
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tiles/RandomTile")]
        public static void CreateDirtTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save RandomTile", "RandomTile", "asset", "Save RandomTile", "Assets");
            if (path == "")
            {
                return;
            }

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DirtTile>(), path);
        }
#endif
    }



}
