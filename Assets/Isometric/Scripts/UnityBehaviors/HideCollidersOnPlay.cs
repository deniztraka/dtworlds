
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DTWorlds.UnityBehaviours
{
    public class HideCollidersOnPlay : MonoBehaviour
    {
        

        void Start()
        {
            var tileMapRenderer = GetComponent<TilemapRenderer>();
            var tempColor = tileMapRenderer.material.color;
            

            tileMapRenderer.material.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0 );

        }


    }

}