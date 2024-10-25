using UnityEngine;
using System;

public class Linear:AbEase
{
    public Linear(float time, bool pingpong):base(time, pingpong)
    {
    }

    override public float GetInterploation()
    {
        return GetLinearInterploation();
    }
}