using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeColor2Red();
        Debug.Log(gameObject.name, gameObject);
        //transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition = transform.localPosition + new Vector3(0, 0, 0.4f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition = transform.localPosition + new Vector3(0, 0, -0.4f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition = transform.localPosition + new Vector3(0.4f, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition = transform.localPosition + new Vector3(-0.4f, 0, 0);
        }
        
    }

    public void ChangeColor2Red()
    {
        GameObject cube = new GameObject();
        cube = GameObject.Find("testCube");
        cube.GetComponent<MeshRenderer>().material.color = Color.red;
    }

}