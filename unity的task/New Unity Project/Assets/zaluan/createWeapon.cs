using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class createWeapon : MonoBehaviour
{
    public class Item
    {
        public int id;
        public int displayId;
    }

    public class CSVitem
    {
        public Dictionary<int, Item> Items = new Dictionary<int, Item>();
        public CSVitem()
        {
            Items.Add(10001, new Item() { id = 10001, displayId = 180001 });
            Items.Add(10002, new Item() { id = 10002, displayId = 180002 });
        }
        //  100001 item id --  displayid 1800001 ----resId 就是hashcode，路径
        // 表1 ： item表格 --   表二 display表格 ----表三：resource表格 
    }
    public class Dispaly
    {
        public int id;
        public string resName;
        public int resId;
    }

    public class CSVdisplay
    {
        public Dictionary<int, Dispaly> mDispaly = new Dictionary<int, Dispaly>();
        public CSVdisplay()
        {
            mDispaly.Add(180001, new Dispaly() { id = 180001, resName = "pre_capsule", resId = "pre_capsule".GetHashCode() });
            mDispaly.Add(180002, new Dispaly() { id = 180002, resName = "pre_sphere", resId = "pre_sphere".GetHashCode() });
        }
   
    }

    public class ResInfo
    {
        public int resId;
        public string path;
    }

    public class CSVRes
    {
        public Dictionary<int, ResInfo> mResinfo = new Dictionary<int, ResInfo>();
        public CSVRes()
        {
            mResinfo.Add("pre_capsule".GetHashCode(), new ResInfo() { resId = "pre_capsule".GetHashCode(), path = "Assets/pre_capsule.prefab" });
            mResinfo.Add("pre_sphere".GetHashCode(), new ResInfo() { resId = "pre_sphere".GetHashCode(), path = "Assets/pre_Sphere.prefab" });
        }

    }



     [MenuItem("GameObject/gamelevel/weapon")]
    public static void createweapon()
    {
        
        if(Selection.activeGameObject.name =="weaponRoot")
        {
            GameObject obj = new GameObject("100001");
            obj.transform.SetParent(Selection.activeGameObject.transform);
            //设置在屏幕中间
         
            Camera[] cameras = SceneView.GetAllSceneCameras();   //拿到所有的相机，返回的结果是相机数组；
            for(int i=0; i<cameras.Length; i++)
            {
                if (Physics.Raycast(cameras[i].transform.position, cameras[i].transform.forward, out RaycastHit raycastHit))
                {
                    obj.transform.position = raycastHit.transform.position;
                }
            }

            CSVitem cSVitem = new CSVitem();
            CSVdisplay cSVdisplay = new CSVdisplay();
            CSVRes cSVRes = new CSVRes();





            Debug.Log("1"); 
        }
        else
        {
            Debug.Log("请在weaponroot节点创建对象");
        }
    }



}
