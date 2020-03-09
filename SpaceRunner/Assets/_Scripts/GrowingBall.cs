using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBall : MonoBehaviour
{
    public float growSpeed;
    Vector3 size;
    public float finalSize;
    public float _delay;
    float _distance;
    public Transform Boss;
    public DroidBoss db;
    bool doOnce = true;

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
        _distance = Vector3.Distance(Boss.transform.position, transform.position);
        if(_distance >= 140)
        {
            if (doOnce)
            {
                StartCoroutine(db.RandomAttack());
                db.randomRot = Random.Range(0, 1);
                doOnce = false;
            }
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(_delay);
        growSpeed = 6;
        //GetComponent<ShotBehavior>().speed = 20;
    }
}
