using UnityEngine;
using System;

public class Main:MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjects;

    private void Start()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {            
            TweenManager.Instance.Move(
                gameObjects[i], 
                gameObjects[i].transform.position, 
                gameObjects[i].transform.position + new Vector3(10, 0, 0),
                i + 1,
                true
            );
        }
    }
}