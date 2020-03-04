using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotx;
    public float roty;
    public float rotz;

    // Update is called once per frame
    void Update()
    {
       transform.Rotate(rotx, roty, rotz);
    }
}
