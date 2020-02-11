using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBall : MonoBehaviour
{
    public float growSpeed;
    Vector3 size;
    public float finalSize;
    public float _delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delay());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (size.x <= finalSize && size.y <= finalSize && size.z <= finalSize)
        {
            size.x += Time.deltaTime * growSpeed;
            size.y += Time.deltaTime * growSpeed;
            size.z += Time.deltaTime * growSpeed;
            transform.localScale = size;
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(_delay);
        growSpeed = 6;
        GetComponent<ShotBehavior>().speed = 15;
    }
}
