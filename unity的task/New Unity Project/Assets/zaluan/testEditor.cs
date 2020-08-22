using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(testClasst))]
public class testEditor : Editor
{
    testClasst mtestClasst;
    DrawLine mdrawline;
    
    private void OnEnable()
    {
        
        mtestClasst = target as testClasst;
        //Vector3 Vec = new Vector3(1, 1, 1);
        mdrawline = mtestClasst.GetComponent<DrawLine>();
        //mtransform = mtestClasst.GetComponent<Transform>();
    }

    public override void OnInspectorGUI()
    {
        bool bChanged = false;
        //base.OnInspectorGUI();
        EditorGUILayout.BeginVertical();


        EditorGUILayout.BeginHorizontal(); //横条开始和结束一定要对应，中间是填写的东西
        int Ex = EditorGUILayout.IntField("INTX", mtestClasst.x);  //界面的名字叫INTX，输入的值是mtestClasst。x
        if(Ex != mtestClasst.x) //一旦数据改变就开始赋值
        {
            bChanged = true;
            mtestClasst.x = Ex;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int Ey = EditorGUILayout.IntField("INTY", mtestClasst.y);
        if(Ey != mtestClasst.y)
        {
            bChanged = true;
            mtestClasst.y = Ey;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int Ez = EditorGUILayout.IntField("INTZ", mtestClasst.z);
        if (Ez != mtestClasst.y)
        {
            bChanged = true;
            mtestClasst.y = Ez;
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();

        float Epower = EditorGUILayout.FloatField("Epower", mtestClasst.power);
        if(Epower != mtestClasst.power)
        {
            bChanged = true;
            mtestClasst.power = Epower;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

        if(bChanged)
        {
            //mdrawline.dir.x = mtestClasst.x;
            //mdrawline.dir.y = mtestClasst.y;
            //mdrawline.dir.z = mtestClasst.z;
            //mdrawline.force = mtestClasst.power;
            //mdrawline.LineDrawing();


            mtestClasst.transform.localPosition = mtestClasst.transform.localPosition +
            new Vector3(mtestClasst.x, mtestClasst.y, mtestClasst.z);


        }
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
