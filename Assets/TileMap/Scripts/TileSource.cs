using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using DTWorlds.Mobiles;
using UnityEngine;

namespace DTWorlds.TileMap
{

    public class TileSource : MonoBehaviour, IInteractable
    {
        public int Source;

        public void Interact(BaseMobile baseMobile)
        {            
            Debug.Log("Interacted with rock tile");

            //TODO: Check if mobile is equipping pickaxe
          
            if (Source < 0)
            {
                //TODO: create popup that says there is no source left on this rock
                Debug.Log("There is no source left on this rock");
                return;
            }

            //TODO: Add source item to mobile's backpack
            Source--;
            Debug.Log(Source);
        }

        

        // Start is called before the first frame update
        void Start()
        {
            Source = 100;
        }
    }
}
