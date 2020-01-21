using System.Collections;
using System.Collections.Generic;
using DTWorlds.TileMap.Tiles;
using DTWorlds.UnityBehaviours;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DTWorlds.TileMap
{
    public class TileMapHandler : MonoBehaviour
    {
        private Tilemap tilemap;

        public PlayerBehaviour PlayerBehaviour;

        private void Start()
        {
            tilemap = gameObject.GetComponent<Tilemap>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0;
                var cell = tilemap.WorldToCell(worldPosition);
                //var cell = tilemap.WorldToCell(PlayerBehaviour.transform.position);

                if (cell == null)
                {
                    return;
                }

                var tile = tilemap.GetTile<DTTile>(cell);
                if (tile == null)
                {
                    return;
                }

                Debug.Log(tile);
            }
        }

    }



}
