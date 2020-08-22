using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GUItest : EditorWindow
{
    static GUISkin skin;

    [MenuItem("Test/test1")]
    static void Test() //这个是静态函数，写哪里都可以，需要依靠这个触发
    {
        skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/New GUISkin.guiskin");
        GetWindowWithRect<GUItest>(new Rect(200, 200, 540, 800));  //getwindowwithrect这个函数是editorwindow的成员函数；
    }
    private void OnGUI()  //触发以后才可以在gui里面画
    {
        EditorGUI.DrawRect(new Rect(12, 148, 540, 2), Color.gray);
        EditorGUI.DrawRect(new Rect(12, 470, 540, 0), Color.gray);
        EditorGUI.DrawRect(new Rect(12, 693, 540, 0), Color.gray);
        EditorGUI.DrawRect(new Rect(12, 12, 540, 175), Color.black);
        EditorGUI.DrawRect(new Rect(12, 195, 540, 100), Color.black);
        EditorGUI.DrawRect(new Rect(12, 298, 540, 122), Color.black);
        EditorGUI.DrawRect(new Rect(12, 423, 540, 83), Color.black);
        EditorGUI.DrawRect(new Rect(12, 515, 540, 225), Color.black);
        EditorGUI.DrawRect(new Rect(12, 739, 540, 52), Color.black);

        //button
        float width = 119;
        float height = 28;
        GUI.Button(new Rect(580 / 2 - 119 / 2, 155, 119, 28), "添加场景");
        GUI.Button(new Rect(580 / 2 - 119 / 2, 475, 119, 28), "添加背景");
        GUI.Button(new Rect(580 / 2 - 119 / 2 - 15, 745, 119 + 30, 28 + 10), "保存");
        GUI.Button(new Rect(330 + width, 765, 100, 20), "修正旧版数据");
        GUI.Button(new Rect(580 / 2 - 119 / 2, 700, 119, 28), "添加形象");


        //label
        GUI.Label(new Rect(15, 15, 63, 14 + 5), "场景列表", skin.label);
        GUI.Label(new Rect(15, 300, 63, 14 + 5), "场景列表", skin.label);
        GUI.Label(new Rect(15, 425, 63, 14 + 5), "场景列表", skin.label);
        GUI.Label(new Rect(15, 520, 49, 14 + 5), "场景列表", skin.label);
        GUI.Label(new Rect(180, 445, 21, 14 + 5), "高", skin.label);
        GUI.Label(new Rect(360, 445, 21, 14 + 5), "宽", skin.label);


        //label rotate
        GUILayout.BeginArea(new Rect(20, 225, 370, 20));
        GUILayout.BeginHorizontal();  //所有东西横着排列，在area区域的东西
        Vector3 val = new Vector3(1, 1, 1);
        GUILayout.Label("rotation", EditorStyles.helpBox, GUILayout.Width(50));
        val = EditorGUILayout.Vector3Field("", val, GUILayout.Width(250));
        GUI.SetNextControlName("reset");
        bool b = GUILayout.Button("reset", GUILayout.Width(45));



        GUILayout.EndHorizontal();
        GUILayout.EndArea();



        //    //GUILayout.Button("A Button with fixed width", GUILayout.Width(100), GUILayout.Height(100));
        //}
        //// Start is called before the first frame  GUILayout.Width(100)









    }
}

