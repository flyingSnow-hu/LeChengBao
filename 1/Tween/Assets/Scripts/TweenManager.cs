using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct EasePosition
{
    public GameObject gameObject;
    public IEase ease;
    public Vector3 begin;
    public Vector3 end;

    public readonly bool Move()
    {
        if (ease.IsCompleted())
        {
            return true;
        }

        Vector3 crntPos = begin + (end - begin) * ease.GetInterploation();
        gameObject.transform.position = crntPos;

        return false;
    }
}

public class TweenManager : MonoBehaviour
{
    public static TweenManager Instance;
    public void Move(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong)
    {
        IEase ease = new Linear(time, pingpong);
        Instance.EaseObjects.Add(new EasePosition(){gameObject=gameObject, ease = ease, begin=begin, end = end});
	}
    public void MoveEaseIn(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong)
    {
        IEase ease = new EaseIn(time, pingpong);
        Instance.EaseObjects.Add(new EasePosition(){gameObject=gameObject, ease = ease, begin=begin, end = end});
	}
    public void MoveEaseOut(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong)
    {
        IEase ease = new EaseOut(time, pingpong);
        Instance.EaseObjects.Add(new EasePosition(){gameObject=gameObject, ease = ease, begin=begin, end = end});
	}
    public void MoveEaseInOut(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong)
    {
        IEase ease = new EaseInOut(time, pingpong);
        Instance.EaseObjects.Add(new EasePosition(){gameObject=gameObject, ease = ease, begin=begin, end = end});
	}

    private HashSet<EasePosition> EaseObjects = new HashSet<EasePosition>();

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        foreach (var easeObj in EaseObjects)
        {
            easeObj.Move();
        }
    }
}
