using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public Vector3 dir = new Vector3();
    public float force = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.A))
        //{
        //    LineDrawing();
        //}
    }

    public void LineDrawing()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 p1 = dir * force * i * 0.5f;
            Vector3 p2 = 0.5f * Vector3.down * 9.8f * i * i * 0.5f * 0.5f;
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.position = p1 + p2;
        }
    }

}
