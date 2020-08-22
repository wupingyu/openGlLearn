using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MaterialEditor : EditorWindow
{
    
    int tempid = 0;
    private string m_sceneName;
    private List<string> m_modelNameList;
    private List<string> m_materialName;
    private List<int> m_materialId;
    private int m_filterIndex = 0;
    private string[] mNameArray;
    private Vector2 m_scrolPos = new Vector2(10, 85);
    private int indexMaterialPopup = 0;
    public static GUISkin m_skin;
    private List<string> m_batchList;
    private int m_batchIndex;


    private class materialtoModel
    {
        public materialtoModel(string mname, int id, string moname,string e)
        {
            materialName = mname;
            materialId = id;
            modelName = moname;
            explanation = e;
        }
        public materialtoModel()
        {

        }
        public string materialName;
        public int materialId;
        public string modelName;
        public string explanation;
    }

    Dictionary<string, materialtoModel> m_materialtomodelCsv;
    public MaterialEditor()
    {
        m_modelNameList = new List<string>();
        m_materialId = new List<int>(4) { 10, 11, 12, 13 };
        m_materialName = new List<string>(4) { "default", "stone", "steel", "wood" };
        m_materialtomodelCsv = new Dictionary<string, materialtoModel>();
        m_batchList = new List<string>();
    }





    [MenuItem("Light37编辑器/材质编辑器")]
    public static void SMEditor()
    {
        m_skin = AssetDatabase.LoadAssetAtPath<GUISkin>(@"Assets\EditorLearnMat\scene_materialeditor.guiskin");
        GetWindowWithRect<MaterialEditor>(new Rect(100, 100, 640, 600));

    }
    private void OnEnable()
    {
        
        m_sceneName = SceneManager.GetActiveScene().name;
        var colliders = GameObject.FindObjectsOfType<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if(!m_modelNameList.Contains(colliders[i].transform.parent.name))
            {
                m_modelNameList.Add(colliders[i].transform.parent.name);
            }
           
        }


        for (int j = 0; j < m_modelNameList.Count; j++)
        {
            if (!m_materialtomodelCsv.TryGetValue(m_modelNameList[j], out materialtoModel mtm))
            {
                
                var kk = new materialtoModel("default", 10, m_modelNameList[j],"empty");
                m_materialtomodelCsv.Add(m_modelNameList[j], kk);
            }

        }

        mNameArray = m_materialName.ToArray();
    }
    private void OnGUI()
    {
        DrawRectangle(new Vector2(10, 10), new Vector2(620, 40), Color.black);
        DrawRectangle(new Vector2(10, 50), new Vector2(620, 30), Color.black);
        DrawRectangle(new Vector2(10, 85), new Vector2(620, 500), Color.black);

        //第一列
        GUILayout.BeginArea(new Rect(20, 20, 350, 30));
        GUILayout.BeginHorizontal();
        GUILayout.Label("mapID:", GUILayout.Width(50));
        GUILayout.Label(m_sceneName, GUILayout.Width(100));
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        //第二列
        GUILayout.BeginArea(new Rect(20, 60, 600, 40));
        GUILayout.BeginHorizontal();
        GUILayout.Label("模型文件名字", GUILayout.Width(160));
        GUILayout.Label("材质选择", GUILayout.Width(55));
        m_filterIndex = EditorGUILayout.Popup(m_filterIndex, mNameArray, GUILayout.Width(60));
        GUILayout.Label("", GUILayout.Width(100));
        GUILayout.Label("备注", GUILayout.Width(200));
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        //做滑块
        Rect rect = new Rect(10, 85, 620, 500);
        bool m_forceHorizontal = false;
        bool m_forceVertical = true;
        GUILayout.BeginArea(rect);
        m_scrolPos = GUILayout.BeginScrollView(m_scrolPos, m_forceHorizontal, m_forceVertical);

        //开始竖着画
        GUILayout.BeginVertical();
        for(int i=0;i<m_modelNameList.Count;i++)
        {
            string nameCo = m_modelNameList[i];
            //materialtoModel tt = new materialtoModel();
            var tt = m_materialtomodelCsv[m_modelNameList[i]];
            //做过滤的功能 当选择某一项，就只出现哪个材质的东西
            if(m_materialId.IndexOf(tt.materialId) != m_filterIndex && m_filterIndex != 0)
            {
                continue;
                //当选择的index
            }

            



            //横着画名字备注等
            GUILayout.BeginHorizontal(GUILayout.Width(300));

            //做多选功能 按住control按键，多选操作
            //按下control按键之后选择的颜色要发生改变 



           
            
            bool a = m_batchList.Contains(nameCo);//判断现在的nameco是不是被选中的
            //名字
            bool b = GUILayout.Button(nameCo, a ? m_skin.label:GUI.skin.label,
                GUILayout.Width(180));

            //记录下哪些名字是按住contrl键多选的 使用一个m_batchlist记录下来
            if (b) //如果按下模型名字的按钮
            {
                if(Event.current.shift)
                {
                    if (m_batchIndex<=i)
                    {
                        for(int k = m_batchIndex; k<=i; k++)
                        {

                        }

                    }
                }

                else if(Event.current.control)//如果按下control键
                {
                    if(m_batchList.Contains(nameCo))
                    {
                        m_batchList.Remove(nameCo);//点击错了，已经选择了的需要去掉
                    }
                    else
                    {
                        m_batchList.Add(nameCo);//contrl 把名字添加到list中
                    }
                    m_batchIndex = i;
                }
                else
                {
                    m_batchList.Clear();//如果按了contrl之后没有按其他键的时候，就把上一次的list清理掉
                    m_batchList.Add(nameCo);
                    m_batchIndex = i;//没有按下任何contrl和shift的时候，把index和batchlist加上当前的模型名字和当前模型的index
                }
            }



            //材质选择
            
            int index0 = m_materialId.IndexOf(tt.materialId);//取出上一个保存的
            //materialID是材质的一个标号 10 11 12 13 ，tt.materilid给的默认的都是10；
            //在点击界面之后，把选择的index的对应的id传给tt。materialId
            //比如选择2 对应的就是11，那么tt.materialid就由10变为11；
            indexMaterialPopup = EditorGUILayout.Popup(
                index0, mNameArray, GUILayout.Width(90));

            if(indexMaterialPopup != index0)
            {
                for(int j=0; j<m_batchList.Count; j++)
                {
                    var tt1 = m_materialtomodelCsv[m_batchList[j]];
                    tt1.materialId = m_materialId[indexMaterialPopup];
                    //把这个map中的选中的nameco对应的materialid批量更改成上面记录的那个
                    //正常的被点击改变的index2 依旧走下面正常路径，
                    //这一步是把bachlist中的其他materialid也改成正常被点击的这一个
                }
            }



            //把新的index保存下来存在表格中，下一帧画的时候，就会根据新的画
            tt.materialId = m_materialId[indexMaterialPopup];

            //备注
            tt.explanation = EditorGUILayout.TextField(tt.explanation, GUILayout.Width(260));
            
            

            if(GUILayout.Button("position",GUILayout.Width(60)))
            {
                GameObject objP = GameObject.Find(m_modelNameList[i]);
                Transform trs = objP.transform.GetChild(0);
                SceneView view = SceneView.sceneViews[0] as SceneView;
                if(trs!=null)
                {
                    EditorGUIUtility.PingObject(trs);
                    Selection.activeGameObject = trs.gameObject;
                    view.LookAt(trs.position);
                }
            }
            GUILayout.EndHorizontal();


        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
        




    }

    private static void DrawRectangle(Vector2 pos, Vector2 size, Color c, int weight = 1)
    {
        float posx = pos.x;
        float posy = pos.y;
        float sizex = size.x;
        float sizey = size.y;
        EditorGUI.DrawRect(new Rect(posx, posy, sizex, weight), c);
        EditorGUI.DrawRect(new Rect(posx, posy + sizey - weight, sizex, weight), c);
        EditorGUI.DrawRect(new Rect(posx, posy, weight, sizey), c);
        EditorGUI.DrawRect(new Rect(posx + sizex - weight, posy, weight, size.y), c);

    }
}

