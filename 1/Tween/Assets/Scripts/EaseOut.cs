using UnityEngine;
using System;
using UnityEditor.UI;

public class EaseOut:AbEase
{
    public EaseOut(float time, bool pingpong):base(time, pingpong)
    {
    }

    override public float GetInterploation()
    {
        float interploation = GetLinearInterploation();
        return Mathf.Sqrt(interploation);
    }
}