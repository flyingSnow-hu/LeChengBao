using UnityEngine;
using System;
using UnityEditor.UI;

public class Linear:AbEase
{
    public Linear(float time, bool pingpong):base(time, pingpong)
    {
    }

    override public float GetInterploation()
    {
        float turns = (Time.time - start) / time;
        if (pingpong)
        {
            int IntergerPart = Mathf.FloorToInt(turns);
            bool positive = (IntergerPart & 1) == 0;
            float remainPart = turns - IntergerPart;
            if (positive) 
            {
                return remainPart;
            }else
            {
                return 1 - remainPart;
            }
        }else
        {
            return Math.Clamp(turns, 0 , 1);
        }
    }
}