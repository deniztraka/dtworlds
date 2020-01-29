using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTWorlds.Mobiles.MovementInputs
{
    public class BaseAIMovementHandler : MonoBehaviour
    {
        [SerializeField] private AxisOptions axisOptions = AxisOptions.Both;

        public Vector2 Input = Vector2.zero;

        public AxisOptions AxisOptions { get { return AxisOptions; } set { axisOptions = value; } }

        public float Horizontal
        {
            get
            {
                return SnapFloat(Input.x, AxisOptions.Horizontal);
            }
        }
        public float Vertical
        {
            get
            {
                return SnapFloat(Input.y, AxisOptions.Vertical);
            }
        }

        private float SnapFloat(float value, AxisOptions snapAxis)
        {
            if (value == 0)
                return value;

            if (axisOptions == AxisOptions.Both)
            {
                float angle = Vector2.Angle(Input, Vector2.up);
                if (snapAxis == AxisOptions.Horizontal)
                {
                    if (angle < 22.5f || angle > 157.5f)
                        return 0;
                    else
                        return (value > 0) ? 1 : -1;
                }
                else if (snapAxis == AxisOptions.Vertical)
                {
                    if (angle > 67.5f && angle < 112.5f)
                        return 0;
                    else
                        return (value > 0) ? 1 : -1;
                }
                return value;
            }
            else
            {
                if (value > 0)
                    return 1;
                if (value < 0)
                    return -1;
            }
            return 0;
        }
    }
}