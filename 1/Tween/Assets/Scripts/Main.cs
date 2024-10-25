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
            bool pingpong = i >= 4;
            switch(i % 4)
            {
                case 0:
                    // 线性:                    
                    TweenManager.Instance.Move(
                        gameObjects[i], 
                        gameObjects[i].transform.position, 
                        gameObjects[i].transform.position + new Vector3(10, 0, 0),
                        3,
                        pingpong
                    );
                    break;
                case 1:
                    // 淡入:                    
                    TweenManager.Instance.MoveEaseIn(
                        gameObjects[i], 
                        gameObjects[i].transform.position, 
                        gameObjects[i].transform.position + new Vector3(10, 0, 0),
                        3,
                        pingpong
                    );
                    break;
                case 2:
                    // 淡出:                    
                    TweenManager.Instance.MoveEaseOut(
                        gameObjects[i], 
                        gameObjects[i].transform.position, 
                        gameObjects[i].transform.position + new Vector3(10, 0, 0),
                        3,
                        pingpong
                    );
                    break;
                case 3:
                    // 淡入淡出:                    
                    TweenManager.Instance.MoveEaseInOut(
                        gameObjects[i], 
                        gameObjects[i].transform.position, 
                        gameObjects[i].transform.position + new Vector3(10, 0, 0),
                        3,
                        pingpong
                    );
                    break;
            }
        }
    }
}