using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MergeTset 
{
    [MenuItem("测试/A")]

    public static void Runtest()
    {
        MeshRenderer[] mrd = GameObject.FindObjectsOfType<MeshRenderer>();
        foreach(var item in mrd)
        {
            return;
        }

    }


}
