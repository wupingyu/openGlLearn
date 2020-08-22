using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("World Euler Angle" + transform.rotation.x);
        Debug.Log("World Euler Angle" + transform.rotation.y);
        Debug.Log("World Euler Angle" + transform.rotation.z);

        Debug.Log("World Euler Angle" + transform.localEulerAngles.x);
        Debug.Log("World Euler Angle" + transform.localEulerAngles.y);
        Debug.Log("World Euler Angle" + transform.localEulerAngles.z);

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(1, 1, 1), 45 * Time.deltaTime, Space.World);
    }
}
