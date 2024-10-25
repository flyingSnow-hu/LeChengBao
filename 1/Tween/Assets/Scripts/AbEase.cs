using UnityEngine;
using System;

public abstract class AbEase:IEase
{
    protected float start;
    // protected float end;
    protected float time;
    protected bool pingpong;

    public AbEase(float time, bool pingpong)
    {
        this.time = time;
        this.pingpong = pingpong;
    }

    virtual public float GetInterploation()
    {
        return 0;
    }

    public bool IsCompleted()
    {
        if (pingpong)
        {
            return false;
        }
        return (Time.time - start) > time;
    }

    
    protected float GetLinearInterploation()
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