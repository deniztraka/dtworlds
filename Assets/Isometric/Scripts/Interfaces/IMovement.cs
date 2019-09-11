using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DTWorlds.Interfaces
{
    public interface IMovement
    {
        float MovementSpeed { set; }

        GameObject GameObject { set; }

        IMovementAxis MovementAxis { get; }

        int? Move(float? speed);
        void Initialize(GameObject gameObject, float movementSpeed);
    }
}

