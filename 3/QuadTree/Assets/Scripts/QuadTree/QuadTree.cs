using UnityEngine;
using System.Collections.Generic;
using System;

public class QuadTreeNode
{
    public const int MAX_DEPTH = 10;
    public const int NorthEast = 0;
    public const int NorthWest = 1;
    public const int SouthWest = 2;
    public const int SouthEast = 3;
    public Rect rect;

    public QuadTreeNode parent;
    public QuadTreeNode[] children;
    public List<GameObject> gameObjects;
    public int depth;
    public int quadrant;

    public bool IsVisible = false;

    public QuadTreeNode(QuadTreeNode parent = null, int depth = 0, int quadrant = 0)
    {
        children = new QuadTreeNode[4];
        gameObjects = new List<GameObject>();
        this.depth = depth;
        this.quadrant = quadrant;
        this.parent = parent;

        if(parent != null)
        {
            rect = GetQuadrantRect(parent.rect, quadrant);
        }
    }

    public bool AddObject(GameObject gameObject)
    {
        var gameObjectBounds = gameObject.GetComponent<Renderer>().bounds;
        var gameObjectRect = new Rect(gameObjectBounds.min.x, gameObjectBounds.min.z, gameObjectBounds.size.x, gameObjectBounds.size.z);

        if(depth == MAX_DEPTH)
        {
            // 达到最大深度，将该物体添加到父节点中并返回
            gameObjects.Add(gameObject);
            return true;
        }

        int quadrant = -1;
        for (int q = 0; q < 4; q++)
        {
            var quadrantRect = GetQuadrantRect(rect, q);

            if(quadrantRect.Overlaps(gameObjectRect))
            {
                if(quadrant == -1)
                {
                    quadrant = q;
                }else{
                    // 如果有多个物体相交，将该物体添加到父节点中并返回
                    gameObjects.Add(gameObject);
                    return true;
                }
            }
        }

        if(quadrant == -1)
        {
            // 该物体与父节点的矩形不相交
            return false;
        }

        // 递归添加物体到子节点中
        if(children[quadrant] == null)
        {
            children[quadrant] = new QuadTreeNode(this, depth + 1, quadrant);
        }

        return children[quadrant].AddObject(gameObject);
    }

    private Rect GetQuadrantRect(Rect parentRect, int quadrant)
    {
        switch (quadrant)
        {
            case QuadTreeNode.NorthEast:
                return new Rect(parentRect.center.x, parentRect.center.y, parentRect.size.x / 2, parentRect.size.y / 2);
            case QuadTreeNode.NorthWest:
                return new Rect(parentRect.min.x, parentRect.center.y, parentRect.size.x / 2, parentRect.size.y / 2);
            case QuadTreeNode.SouthWest:
                return new Rect(parentRect.min.x, parentRect.min.y, parentRect.size.x / 2, parentRect.size.y / 2);
            case QuadTreeNode.SouthEast:
                return new Rect(parentRect.center.x, parentRect.min.y, parentRect.size.x / 2, parentRect.size.y / 2);
        }
        return new Rect();
    }

    public bool IsIntersectedWith(Rect cameraRect){
        return cameraRect.Overlaps(rect);
    }

    internal void ClearVisible()
    {
        IsVisible = false;
        foreach (var child in children)
        {
            child?.ClearVisible();
        }
    }

    internal void CalculateVisible(Rect cameraRect)
    {
        IsVisible = IsIntersectedWith(cameraRect);

        if(!IsIntersectedWith(cameraRect))
        {
            return;
        }

        foreach (var child in children)
        {
            child?.CalculateVisible(cameraRect);
        }

    }

    internal void UpdateVisible()
    {
        foreach (var obj in gameObjects)
        {
            obj.SetActive(IsVisible);
        }

        foreach (var child in children)
        {
            child?.UpdateVisible();
        }
    }

    internal void DrawGizmos()
    {
        Gizmos.color = IsVisible? Color.green : Color.gray;
        Gizmos.DrawWireCube(
            new Vector3(rect.center.x, 0, rect.center.y), 
            new Vector3(rect.size.x, 0, rect.size.y)
        );


        foreach (var child in children)
        {
            child?.DrawGizmos();
        }
    }
}

public class QuadTree
{
    private QuadTreeNode root;

    public QuadTree(int size = 100)
    {
        root = new QuadTreeNode();
        root.rect = new Rect(0 - size / 2, 0 - size / 2, size, size);
    }


    public bool AddObject(GameObject gameObject)
    {
        if(root == null)
        {
            root = new QuadTreeNode();
            root.gameObjects = new List<GameObject>();
        }

        return root.AddObject(gameObject);

    }

    public bool RemoveObject(GameObject gameObject)
    {
        return false;
    }

    public void ClearVisible()
    {
        root.ClearVisible();
    }

    internal void CalculateVisible(Rect cameraRect)
    {
        root.CalculateVisible(cameraRect);
    }

    internal void UpdateVisible()
    {
        root.UpdateVisible();
    }

    internal void DrawGizmos()
    {
        root.DrawGizmos();
    }
}