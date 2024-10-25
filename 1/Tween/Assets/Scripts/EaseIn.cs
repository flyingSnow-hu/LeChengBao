using UnityEngine;
using System;
using UnityEditor.UI;

public class EaseIn:AbEase
{
    public EaseIn(float time, bool pingpong):base(time, pingpong)
    {
    }

    override public float GetInterploation()
    {
        float interploation = GetLinearInterploation();
        return interploation * interploation;
    }
}