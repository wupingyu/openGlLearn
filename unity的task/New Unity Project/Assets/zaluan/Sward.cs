using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sward : MonoBehaviour
{

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Origin = GameObject.Find("Cube");
        GameObject end = GameObject.Find("Cube1");
        Origin.transform.LookAt(end.transform.position,Vector3.up);
    }
}
