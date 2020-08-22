using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;

public class binPackingDuras 
{
    public List<Rect> m_rectsPositionList;
    public List<Texture2D> m_sTexlist;
    public List<string> m_sTexPathList;
    public List<List<Rect>> m_avialable;
    public List<Texture2D> m_bTexList;
    public List<Rect> m_sTexPosition;//小图的位置信息
    public Dictionary<Texture2D, int> m_smallTobig;
    public string m_path;
    public int m_MergeTexSize;

    public binPackingDuras()
    {
        m_rectsPositionList = new List<Rect>();
        m_sTexlist = new List<Texture2D>();
        m_avialable = new List<List<Rect>>();
        m_bTexList = new List<Texture2D>();
        m_sTexPosition = new List<Rect>();
        m_smallTobig = new Dictionary<Texture2D, int>();
        m_sTexPathList = new List<string>();
    }

    public void Init(string path,int MergeTexSize)
    {
        m_path = path;
        m_MergeTexSize = MergeTexSize;

    }



    [MenuItem("程序工具/binPacking")]
    
    public static void binPacking()
    {
        binPackingDuras packingDuras = new binPackingDuras();
        string path = @"Assets\LearnTextureMerge";
        packingDuras.Init(path, 1024);
        packingDuras.Packages();
      

    }


    
    //读取一个文件夹中的所有图片
    public void Director(string path)
    {
        string[] files = Directory.GetFiles(@path);
        foreach (var item in files)
        {
            if(item.Contains("meta"))
            {
                continue;
            }
            if (item.Contains("tex"))
            {
                
                Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(item) as Texture2D;
                if (tex != null)
                {
                    m_sTexlist.Add(tex);
                    m_sTexPathList.Add(item);
                }
                
            }
        }
       
    }
    public void Packages()
    {
        Director(m_path);
        for ( int i = 0; i < m_sTexlist.Count; i++)
        {
            Texture2D tex = m_sTexlist[i];
            addOneTex(tex,m_MergeTexSize);
        }
        //SetPixels();
        SetPixelsInRenderer();
        SaveBtex();

    }



    public void addOneTex(Texture2D tex,int mergeTexSize)
    {
        
        if (m_avialable.Count==0)//如果所有的区域都用完了，就新添加一个大图在m_available中
        {
            List<Rect> r = new List<Rect>();
            r.Add(new Rect(0, 0, m_MergeTexSize, m_MergeTexSize));
            m_avialable.Add(r);
            m_bTexList.Add(new Texture2D(m_MergeTexSize, m_MergeTexSize));
        }

        bool isSuccess = false;
        int index = 0;
        //m_avialable 二维数组，每个大图中可用的小矩形
        for (int i = 0; i < m_avialable.Count && !isSuccess; i++)
        {
            
            index = i;
            BestAreaFit(m_avialable[i], tex, out int optimalIndex);
            
            if (optimalIndex != -1)
            {
                Rect rect = m_avialable[i][optimalIndex]; //取出来的可用的RECT
                isSuccess = true;
                Rect re = new Rect(rect.x, rect.y, tex.width, tex.height);
                m_sTexPosition.Add(re);
                m_avialable[i].RemoveAt(optimalIndex);//移除remove可能会导致移除不掉，使用removeat更好
                m_smallTobig.Add(tex, i);

                BestSplit(rect, re, out Rect optimal1, out Rect optimal2);
                m_avialable[i].Add(optimal1);
                m_avialable[i].Add(optimal2);               
                break;
            }
            //for(int j=0;j<m_avialable[i].Count;j++)
            //{
            //    Rect rect = m_avialable[i][j]; //取出来的可用的RECT
            //    if (rect.width >= tex.width && rect.height >= tex.height)
            //    {


            //        isSuccess = true;
            //        Rect re = new Rect(rect.x, rect.y, tex.width, tex.height);
            //        m_sTexPosition.Add(re);
            //        m_avialable[i].RemoveAt(j);//移除remove可能会导致移除不掉，使用removeat更好
            //        m_smallTobig.Add(tex,i);

            //        BestSpllit(rect, re, out Rect optimal1, out Rect optimal2);
            //        m_avialable[i].Add(optimal1);
            //        m_avialable[i].Add(optimal2);
            //        //m_avialable[i].Add(new Rect(rect.x + tex.width, rect.y, rect.width - tex.width, tex.height));
            //        //m_avialable[i].Add(new Rect(rect.x, rect.y + tex.height, rect.width, rect.height - tex.height));
            //        break;
            //    }

            //}
        }
        if (!isSuccess)
        {
            List<Rect> r = new List<Rect>();
            r.Add(new Rect(0, 0, m_MergeTexSize, m_MergeTexSize));
            m_bTexList.Add(new Texture2D(m_MergeTexSize, m_MergeTexSize));
            m_avialable.Add(r);

            Rect rect = new Rect(0, 0, m_MergeTexSize, m_MergeTexSize);
            //直接不添加，可能存在移除的时候，浮点数的误差导致移除不掉
            Rect re = new Rect(0, 0, tex.width, tex.height);
            m_sTexPosition.Add(re);
            m_smallTobig.Add(tex, index + 1);
            BestSplit(rect, re, out Rect optimal1, out Rect optimal2);
            m_avialable[index + 1].Add(optimal1);
            m_avialable[index + 1].Add(optimal2);
            //m_avialable[index + 1].Add(new Rect( tex.width, 0, m_MergeTexSize - tex.width, tex.height));
            //m_avialable[index + 1].Add(new Rect(0, tex.height, m_MergeTexSize, m_MergeTexSize - tex.height));
            m_avialable[index + 1].Remove(rect);
            

        }

    }
    public void BestSplit(Rect Big, Rect Tex, out Rect Optimal1,out Rect Optimal2)
    {
        //横着画
        Rect rect1 = new Rect(Big.x + Tex.width, Big.y, Big.width - Tex.width, Tex.height);
        Rect rect2 = new Rect(Big.x, Big.y + Tex.height, Big.width, Big.height - Tex.height);
        float area1 = (Big.width - Tex.width) * Tex.height;
        float ares2 = Big.width * (Big.height - Tex.height);
        float differ1 = Math.Abs(area1 - ares2);

        //竖着画
        Rect rect3 = new Rect(Big.x + Tex.width, Big.y, Big.width - Tex.width, Big.height);
        Rect rect4 = new Rect(Big.x, Big.y + Tex.height, Tex.width, Big.height - Tex.height);
        float area3 = (Big.width - Tex.width) * Big.height;
        float area4 = Tex.width * (Big.height - Tex.height);
        float differ2 = Math.Abs(area3 - area4);

        if(differ1<=differ2)
        {
            Optimal1 = rect1;
            Optimal2 = rect2;
        }
        else
        {
            Optimal1 = rect3;
            Optimal2 = rect4;
        }
        

    }

    public void BestAreaFit(List<Rect> available,Texture2D tex,out int optimalIndex)
    {
        optimalIndex = -1;
        float areaDiffer = float.MaxValue;
        for(int i=0; i<available.Count;i++)
        {
            Rect rect = available[i];
            if(rect.width>=tex.width && rect.height>= tex.height)
            {
                float areaDiffer1 = (rect.width * rect.height) - (tex.width * tex.height);
                if(areaDiffer1<areaDiffer)
                {
                    areaDiffer = areaDiffer1;
                    optimalIndex = i;
                }
            }
        }

    }


    public void SetPixels()
    {
        for (int j = 0; j < m_sTexPathList.Count; j++)
        {
            TextureImporter importer = TextureImporter.GetAtPath(m_sTexPathList[j]) as TextureImporter;
            importer.isReadable = true;
            importer.SaveAndReimport();
            Texture2D texS = m_sTexlist[j];
            Color[] c = texS.GetPixels();
            int BIndex;
            m_smallTobig.TryGetValue(texS,out BIndex);
            Rect rect = m_sTexPosition[j];
            Texture2D texB = m_bTexList[BIndex];
            texB.SetPixels((int)rect.x,(int)rect.y,(int)rect.width,(int)rect.height, c);
           
        }
    }

    public void SetPixelsInRenderer()
    {
        var currentRT = RenderTexture.active; 
         Material mat = new Material(Shader.Find("Unlit/MergeTexColor"));
        Dictionary<int, RenderTexture> rts = new Dictionary<int, RenderTexture>();
        //同样大小的图用同一个renderTexture
        for(int i=0; i<m_bTexList.Count;i++)
        {
            RenderTexture rt = null;
            Texture2D atlas = m_bTexList[i];
            if(!rts.TryGetValue((atlas.width << 16 & atlas.height),out rt))
            {
                rt = new RenderTexture(atlas.width, atlas.height, 0, RenderTextureFormat.ARGB32);
                rts.Add(atlas.width << 16 & atlas.height, rt);
            }
            Graphics.SetRenderTarget(rt);//拿出一张大图，画一张空白的大图，把shader中的properties赋值
            mat.SetVector("_DestRect", new Vector4(0, 0, atlas.width, atlas.height));
            mat.SetVector("_DestSrcSize", new Vector4(atlas.width, atlas.height, atlas.width, atlas.height));
            mat.SetColor("_Color", Color.clear);
            //blit之前的参数是为blit准备的，画大图的时候，用clear清楚所有颜色
            Graphics.Blit(atlas, rt, mat);//???
            //blit存在两种变体，把atlas纹理画到rt上去，使用material可以加上shader自己来控制怎么画这个图
            mat.SetColor("_Color", Color.white);
            //white是白色，111和任何色相×都是原来的颜色
            int m = 0;
            foreach(var item in m_smallTobig.Values)
            {
                //把属于同一张大图的小图取出来 画在大图上
                if(item.Equals(i))
                {
                    Texture2D subTex = m_sTexlist[m];
                    Rect subRect = m_sTexPosition[m];
                    mat.SetVector("_DestRect", new Vector4(subRect.x, subRect.y, subRect.width, subRect.height));
                    mat.SetVector("_DestSrcSize", new Vector4(atlas.width, atlas.height, subRect.width, subRect.height));
                    Graphics.Blit(subTex, rt, mat);

                }
                m++;
            }
            atlas.ReadPixels(new Rect(0, 0, atlas.width, atlas.height), 0, 0, false);
            //读取当前rt的像素放到atlas中去；
            atlas.Apply(); //把像素设置到纹理中的数据

        }

        RenderTexture.active = currentRT;
        foreach(var kv in rts)
        {
            RenderTexture.DestroyImmediate(kv.Value);
        }
        rts.Clear();
        Material.DestroyImmediate(mat);


    }



    public void SaveBtex()
    {
        for(int i=0;i<m_bTexList.Count;i++)
        {
            string path1 = m_path.Replace("\\", "/");
            string path = string.Format("{0}/MergeTex/{1}.png", path1, i);
            System.IO.File.WriteAllBytes(path, m_bTexList[i].EncodeToPNG());
        }
    }
        

}
