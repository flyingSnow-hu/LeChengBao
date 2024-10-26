using UnityEditor;
using System;

public struct QuadTreeNode
{
    public QuadTreeNode parent;
    public QuadTreeNode[] children;
    public List<GameObject> gameObjects;
    public bool IsIntersectedWith(Rect cameraRect){}
}

public class QuadTree
{
    private QuadTreeNode root;
    public bool AddObject(GameObject gameObject){}
    public bool RemoveObject(GameObject gameObject){}
    public QuadTreeNode[] GetIntersections(Rect cameraRect){}
}

public class SceneManager
{
    private QuadTree quadTree;

    public void MoveCamera(Vector3 target)
    {
        Rect cameraRect = GetCameraRect();
        QuadTreeNode[] intersections = GetIntersections(cameraRect);

        foreach(gameObject in gameObjects)
        {
            gameObject.SetActive(gameObject.treeNode.IsVisible());
        }
    }
}
