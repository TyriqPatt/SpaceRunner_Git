using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public GameObject Child;
    public float speed;
    float rot;
    float rot2;
    float rot3;
    public bool IsInGroup;
    public Vector3 size;
    public float finalSize;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        rot = Random.Range(-5, 5);
        rot2 = Random.Range(-5, 5);
        rot3 = Random.Range(-5, 5);
        transform.localScale = new Vector3(0, 0, 0);
        if (!IsInGroup)
        {
            speed = Random.Range(1, 2);
            finalSize = Random.Range(3, 5);
            Child = transform.GetChild(0).gameObject;
        }
        else
        {
            speed = 2;
            finalSize = Random.Range(1, 11);
            if (finalSize > 2 && finalSize < 8)
            {
                finalSize = 1;
            }
            if (finalSize > 7 && finalSize < 11)
            {
                finalSize = 2;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0)
        {
            if (!IsInGroup)
            {
                if (Child == null)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Child.transform.Rotate(rot, rot2, rot3);
                    //transform.Translate(0, 0, -speed);
                    if (delay < 1.5f)
                    {
                        delay += Time.deltaTime;
                        if (size.x <= finalSize && size.y <= finalSize && size.z <= finalSize)
                        {
                            size.x += Time.deltaTime * .5f;
                            size.y += Time.deltaTime * .5f;
                            size.z += Time.deltaTime * .5f;
                            transform.localScale = size;
                        }
                    }
                    if (delay >= 1.5f)
                    {
                        transform.Translate(0, 0, speed);
                        //transform.parent = null;
                    }
                }
            }
            else
            {
                transform.Rotate(rot, rot2, rot3);
                if (size.x <= finalSize && size.y <= finalSize && size.z <= finalSize)
                {
                    size.x += Time.deltaTime * .5f;
                    size.y += Time.deltaTime * .5f;
                    size.z += Time.deltaTime * .5f;
                    transform.localScale = size;
                }
            }
        } 
    }
}
