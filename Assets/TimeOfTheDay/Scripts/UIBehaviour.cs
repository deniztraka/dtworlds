using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TimeOfTheDay
{
    public abstract class UIBehaviour : MonoBehaviour
    {
        public GameTimeHandler GameTime;

        // Start is called before the first frame update
        void Awake()
        {
            if (GameTime != null)
            {
                GameTime.OnAfterValueChangedEvent += new GameTimeHandler.GameTimeEventHandler(UpdateMe);
            }
        }
        public abstract void UpdateMe();
    }
}
