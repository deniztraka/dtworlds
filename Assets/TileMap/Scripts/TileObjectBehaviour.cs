using System;
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using DTWorlds.Mobiles;
using DTWorlds.UnityBehaviours;
using SpriteGlow;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DTWorlds.TileMap
{

    public class TileObjectBehaviour : MonoBehaviour, IInteractable
    {
        private SpriteRenderer spriteRenderer;
        private PlayerBehaviour playerBehaviour;

        private SpriteGlowEffect spriteGlowEffect;

        public int Source;
        public bool IsSelected;

        public Sprite SelectedSprite;

        public void Start()
        {
            Source = 100;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteGlowEffect = GetComponent<SpriteGlowEffect>();
            spriteGlowEffect.enabled = false;            
        }

        void OnMouseDown()
        {
            if (playerBehaviour == null)
            {
                playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
            }

            if (!IsSelected)
            {
                playerBehaviour.SetTarget(this);
            }
            else
            {
                playerBehaviour.SetTarget(null);
                SetSelected(false);
            }
        }

        internal void SetSelected(bool val)
        {

            IsSelected = val;
            if (IsSelected)
            {
                spriteRenderer.sprite = SelectedSprite;
                spriteGlowEffect.enabled = true;
            }
            else
            {
                spriteGlowEffect.enabled = false;
                spriteRenderer.sprite = null;
                
            }
        }

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
    }
}
