using UnityEngine;

public class SceneManager: MonoBehaviour
{
    public static SceneManager Instance;
    public const int SCENE_SIZE = 100;

    private void Awake()
    {
        Instance = this;
        quadTree = new QuadTree(SCENE_SIZE);
    }
    

    private QuadTree quadTree;

    private void Start()
    {

        for (int i = 0; i < 10000; i++)
        {                
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.name = $"Cube_{i}";
            obj.transform.SetParent(transform);

            var halfSize = SCENE_SIZE / 2;
            obj.transform.position = new Vector3(Random.Range(-halfSize, halfSize), 0, Random.Range(-halfSize, halfSize));
            AddObject(obj);
        }
    }

    public void AddObject(GameObject obj)
    {
        quadTree.AddObject(obj);
    }

    public void RemoveObject(GameObject obj)
    {
        quadTree.RemoveObject(obj);
    }

    public void MoveCamera(Vector3 target)
    {
        Camera camera = Camera.main;
        camera.transform.position = target;

        Rect cameraRect = GetCameraRect();

        quadTree.ClearVisible();
        quadTree.CalculateVisible(cameraRect);
        quadTree.UpdateVisible();
    }

    public void Update()
    {
        Rect cameraRect = GetCameraRect();

        quadTree.ClearVisible();
        quadTree.CalculateVisible(cameraRect);
        quadTree.UpdateVisible();
    }

    private Rect GetCameraRect()
    {
        Camera camera = Camera.main;

        Vector3 screen00 = camera.ViewportToWorldPoint(new Vector3(0, 0, 1));
        Vector3 screen11 = camera.ViewportToWorldPoint(new Vector3(1, 1, 1));
        Vector3 screen01 = camera.ViewportToWorldPoint(new Vector3(0, 1, 1));
        Vector3 screen10 = camera.ViewportToWorldPoint(new Vector3(1, 0, 1));

        // 获得射线与XZ平面交点
        Vector3 interctionPoint00 = GetIntersectionPointWithXZPlane(camera.transform.position,screen00 - camera.transform.position);
        Vector3 interctionPoint11 = GetIntersectionPointWithXZPlane(camera.transform.position,screen11 - camera.transform.position);
        Vector3 interctionPoint01 = GetIntersectionPointWithXZPlane(camera.transform.position,screen01 - camera.transform.position);
        Vector3 interctionPoint10 = GetIntersectionPointWithXZPlane(camera.transform.position,screen10 - camera.transform.position);

        float minX = Mathf.Min(interctionPoint00.x, interctionPoint11.x, interctionPoint01.x, interctionPoint10.x);
        float maxX = Mathf.Max(interctionPoint00.x, interctionPoint11.x, interctionPoint01.x, interctionPoint10.x);

        float minY = Mathf.Min(interctionPoint00.z, interctionPoint11.z, interctionPoint01.z, interctionPoint10.z);
        float maxY = Mathf.Max(interctionPoint00.z, interctionPoint11.z, interctionPoint01.z, interctionPoint10.z);


        return new Rect(minX, minY, maxX - minX, maxY - minY);
    }

    private Vector3 GetIntersectionPointWithXZPlane(Vector3 original, Vector3 direction)
    {
        float t = -original.y / direction.y;
        if (t <= 0)  
        {
            t = SCENE_SIZE;
        }

        return original + direction * t;
    }

    private void OnDrawGizmos()
    {        
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(
            new Vector3(0, 0, 0), 
            new Vector3(SCENE_SIZE, 0, SCENE_SIZE)
        );
        quadTree?.DrawGizmos();


        Gizmos.color = Color.red;
        var camera = Camera.main;

        Vector3 screen00 = camera.ViewportToWorldPoint(new Vector3(0, 0, 1));
        Vector3 screen11 = camera.ViewportToWorldPoint(new Vector3(1, 1, 1));
        Vector3 screen01 = camera.ViewportToWorldPoint(new Vector3(0, 1, 1));
        Vector3 screen10 = camera.ViewportToWorldPoint(new Vector3(1, 0, 1));

        // 获得射线与XZ平面交点
        Vector3 interctionPoint00 = GetIntersectionPointWithXZPlane(camera.transform.position,screen00 - camera.transform.position);
        Vector3 interctionPoint11 = GetIntersectionPointWithXZPlane(camera.transform.position,screen11 - camera.transform.position);
        Vector3 interctionPoint01 = GetIntersectionPointWithXZPlane(camera.transform.position,screen01 - camera.transform.position);
        Vector3 interctionPoint10 = GetIntersectionPointWithXZPlane(camera.transform.position,screen10 - camera.transform.position);

        interctionPoint00.y = 0;
        interctionPoint11.y = 0;
        interctionPoint01.y = 0;
        interctionPoint10.y = 0;

        Gizmos.DrawSphere(interctionPoint00, 2f);
        Gizmos.DrawSphere(interctionPoint11, 2f);
        Gizmos.DrawSphere(interctionPoint01, 2f);
        Gizmos.DrawSphere(interctionPoint10, 2f);

    }
}
