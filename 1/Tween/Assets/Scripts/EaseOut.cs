using UnityEngine;
using System;

public class EaseOut:AbEase
{
    public EaseOut(float time, bool pingpong):base(time, pingpong)
    {
    }

    override public float GetInterploation()
    {
        float interploation = GetLinearInterploation();
        float minusInterploation = 1 - interploation;
        return 1 - (minusInterploation * minusInterploation);
    }
}