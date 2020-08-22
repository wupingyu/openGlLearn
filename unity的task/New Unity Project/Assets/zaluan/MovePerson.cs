using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePerson : MonoBehaviour
{
    public GameObject Sphere1;
    public GameObject Cylinder1;
    // Start is called before the first frame update
    void Start()
    {
        //把人放在球的最上方；
        float R1 = Sphere1.transform.localScale.x * 0.5f;
        Vector3 V1 = Sphere1.transform.position + new Vector3(0, R1, 0);
        transform.position = V1;
    }

    // Update is called once per frame
    void Update()
    {
       
        if(Input.GetKey(KeyCode.W))
        {
            if(Sphere1.transform.position.y <transform.position.y)
            {
                Vector3 R1 = Vector3.Cross(transform.forward, Sphere1.transform.up);
                transform.RotateAround(Sphere1.transform.position, R1, 2);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Sphere1.transform.position.y < transform.position.y)
            {
                Vector3 R1 = Vector3.Cross(transform.forward, Sphere1.transform.up);
                transform.RotateAround(Sphere1.transform.position, R1, -2);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (Sphere1.transform.position.y < transform.position.y)
            {
                Vector3 L1 = Vector3.Cross(transform.forward, Sphere1.transform.up);
                Vector3 L2 = Vector3.Cross(L1, Sphere1.transform.up);
                transform.RotateAround(Sphere1.transform.position, L2, -2);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Sphere1.transform.position.y < transform.position.y)
            {
                Vector3 L1 = Vector3.Cross(transform.forward, Sphere1.transform.up);
                Vector3 L2 = Vector3.Cross(L1, Sphere1.transform.up);
                transform.RotateAround(Sphere1.transform.position, L2, 2);
            }
        }
        Vector3 crossValue = Vector3.Cross(Sphere1.transform.up, Cylinder1.transform.up).normalized;
        
    }
}
