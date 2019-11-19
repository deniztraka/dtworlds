using System.Collections;
using System.Collections.Generic;
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
                var cell = tilemap.WorldToCell(PlayerBehaviour.transform.position);
                //Debug.Log(cell);
                var tile = tilemap.GetTile(cell);
                Debug.Log(tile);
            }
        }

    }



}
