using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class gamelevel : MonoBehaviour
{

    [MenuItem("GameObject/Gamevel/wind")]   //表示接下来要更改编辑器 会默认的执行下面的static的函数
 
     public static void creatWind()
    {
        GameObject wind = new GameObject("wind");
        wind.transform.localPosition = new Vector3(10, 10, 10);

        GameObject src = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        src.name = "src";
        src.transform.SetParent(wind.transform);
        src.transform.localPosition = Vector3.zero; 
        src.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        

        GameObject dest = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        dest.name = "dest";
        dest.transform.SetParent(wind.transform);
        dest.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        dest.transform.localPosition = new Vector3(1f, 1f, 1f);

        GameObject power = new GameObject("power");
        power.transform.SetParent(wind.transform);

        //画箭头
        GameObject sward = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/pre_sward.prefab"));
        sward.transform.SetParent(wind.transform);
        sward.transform.localPosition = Vector3.zero;
        sward.transform.rotation = Quaternion.FromToRotation(Vector3.forward, dest.transform.position - src.transform.position);//从哪个方向转向哪个方向    
        //在箭头的gameobject上挂一个脚本；更新这个箭头的transform中的rotation发生的改变；
        gamelevelUtil gU = sward.AddComponent<gamelevelUtil>();
        gU.power = 10f; 
        //画线
        List<Vector3> line = CPoint(gU.power, sward.transform.forward, src.transform.position);
        GameObject swardLine = GameObject.CreatePrimitive(PrimitiveType.Quad);
        swardLine.layer = LayerMask.NameToLayer("CustomWorld0");
        swardLine.transform.localPosition = src.transform.position;
        swardLine.GetComponent<MeshFilter>().sharedMesh = CreateLineMesh(line);
        swardLine.GetComponent<MeshRenderer>().sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>(@"Assets\pre_swardLine.prefab");
        swardLine.transform.SetParent(wind.transform);
        swardLine.transform.position = Vector3.zero;
        swardLine.transform.rotation = Quaternion.identity;

    }

    static public List<Vector3> CPoint(float power, Vector3 dir, Vector3 src)
    {
        List<Vector3> line = new List<Vector3>();
        for(int i=0; i<10; i++)
        {
            Vector3 p1 = power * i * 0.2f * dir;
            Vector3 p2 = 0.5f * new Vector3(0, -9.8f, 0) * i * 0.2f * i * 0.2f;
            line.Add(p1 + p2 + src);
            GameObject Obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Obj.transform.position = (p1 + p2 + src);
            Obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            //Obj.GetComponent<Renderer>().material.color = Color.red;

        }
        return line;
    }

    static private Mesh CreateLineMesh(List<Vector3>Points)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[Points.Count * 2];  
        int[] triangles = new int[(Points.Count - 1) * 6];
        Vector2[] uvs = new Vector2[Points.Count * 2];

        float length = 0;
        for(int i=0; i<Points.Count; i++)
        {
            Vector3 Dir;
            if(i>0)
            {
                Dir = Points[i] - Points[i - 1];
                triangles[(i - 1) * 6] = (i - 1) * 2;
                triangles[(i - 1) * 6 + 1] = (i - 1) * 2 +1;
                triangles[(i - 1) * 6 + 2] = i * 2;
                triangles[(i - 1) * 6 + 3] = (i - 1) * 2 + 1;
                triangles[(i - 1) * 6 + 4] = i * 2;
                triangles[(i - 1) * 6 + 5] = i * 2 +1;
            }
            else
            {
                Dir = Points[i+1] - Points[i ];
            }

            Dir.Normalize();
            Vector3 Dir1;
            if(Vector3.Dot(Dir,Vector3.up)<0.9)
            {
                Dir1 = Vector3.Cross(Dir, Vector3.up);
            }
            else
            {
                Dir1 = Vector3.Cross(Dir, Vector3.forward);
            }

            vertices[i * 2 + 0] = Points[i] + Dir * 0.1f;
            vertices[i * 2 + 1] = Points[i] - Dir * 0.1f;

            uvs[i * 2] = new Vector2(0, length);
            uvs[i * 2 + 1] = new Vector2(1, length);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        return mesh;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
