using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missiles : MonoBehaviour
{
    public bool start;
    public GameObject[] Asteroids;
    public GameObject[] Enemies;
    public List<GameObject> targets = new List<GameObject>();
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        //Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject s in Asteroids)
        {
            //s.transform.position -= transform.position;
            if (s.gameObject.layer == 11)
            {
                targets.Add(s.gameObject);
            }
        }
        if (target == null)
        {
            target = targets[Random.Range(0, targets.Count)].transform;
            if (target.position.z > transform.position.z)
            {
                if (target.GetComponent<Health>().CurrentHealth <= 0)
                {
                    target = null;
                }
            }
            else
            {
                target = null;
            }
        }
        //transform.LookAt(target.position);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target.position, 1 * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
