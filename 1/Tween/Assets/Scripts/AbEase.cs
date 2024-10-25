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

    public void Start()
    {
        start = Time.time;
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
}