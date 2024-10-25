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
        return GetLinearInterploation();
    }
}