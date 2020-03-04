using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float TempSmooth = .125f;
    public float smoothspeed = .125f;
    float EdgeSmoothing = .125f;
    public float pos;
    public Vector3 offset;
    public Flight_Controller fc;
    public Transform ship;
    bool Camlook;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(campos());
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(campos());
        }

        Vector3 desiredPostion = target.position + offset;
        Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredPostion, smoothspeed);
        transform.position = smoothedposition;
        if (Camlook)
        {
            transform.LookAt(target);
        }

        if (ship.transform.position.x >= fc.clamppos && ship.transform.position.x > 0)
        {
            smoothspeed = .05f;
            //Debug.Log("Smoothright");
        }
        else if (ship.transform.position.x < fc.clamppos && ship.transform.position.x > 0)
        {
            smoothspeed = EdgeSmoothing;
        }

        if (ship.transform.position.x <= -fc.clamppos && ship.transform.position.x < 0)
        {
            smoothspeed = .05f;
            //Debug.Log("smoothleft");
        }
        else if (ship.transform.position.x > -fc.clamppos && ship.transform.position.x < 0)
        {
            smoothspeed = EdgeSmoothing;
        }
    }

    IEnumerator campos()
    {
        fc.enabled = false;
        smoothspeed = TempSmooth;
        Camlook = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + pos);
        yield return new WaitForSeconds(2);
        Camlook = true;
        smoothspeed = .125f;
        fc.enabled = true;
    }
}
