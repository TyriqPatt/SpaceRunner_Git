using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothspeed = .125f;
    public float Smoothing = .25f;
    public Vector3 offset;
    public Flight_Controller fc;
    public Transform ship;


    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 50);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPostion = target.position + offset;
        Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredPostion, smoothspeed);
        transform.position = smoothedposition;
        transform.LookAt(target);

        if (ship.transform.position.x >= fc.clamppos && ship.transform.position.x > 0)
        {
            smoothspeed = .05f;
            //Debug.Log("Smoothright");
        }
        else if (ship.transform.position.x < fc.clamppos && ship.transform.position.x > 0)
        {
            smoothspeed = Smoothing;
        }

        if (ship.transform.position.x <= -fc.clamppos && ship.transform.position.x < 0)
        {
            smoothspeed = .05f;
            //Debug.Log("smoothleft");
        }
        else if (ship.transform.position.x > -fc.clamppos && ship.transform.position.x < 0)
        {
            smoothspeed = Smoothing;
        }
    }
}
