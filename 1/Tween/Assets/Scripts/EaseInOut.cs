using UnityEngine;
using System;
using UnityEditor.UI;

public class EaseInOut:AbEase
{
    public EaseInOut(float time, bool pingpong):base(time, pingpong)
    {
    }

    override public float GetInterploation()
    {
        float interploation = GetLinearInterploation();
        if(interploation < 0.5f)
        {
            float i2 = (interploation * 2);
            return (i2 * i2) / 2;
        }else
        {            
            float i2 = 2 * (1-interploation);
            return 1 - (i2 * i2) / 2;
        }
    }
}