using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public List<GameObject> Asteroids = new List<GameObject>();
    public GameObject[] Asteriods;
    public GameObject Asteriod;

    // Start is called before the first frame update
    void Start()
    {
        Asteriods = GameObject.FindGameObjectsWithTag("Asteroid");
    }

    // Update is called once per frame
    void Update()
    {
        //Asteriod.transform.position = Vector3.MoveTowards(Asteriod.transform.position, transform.position, 1);

        foreach (GameObject s in Asteriods)
        {
            //s.transform.position -= transform.position;
            s.transform.position = Vector3.MoveTowards(s.transform.position, transform.position, 1);
        }

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 100, transform.forward, out hit, 10))
        {
            if (hit.transform.tag == "Asteroid")
            {
                Asteroids.Add(hit.transform.gameObject);
                hit.transform.parent = transform.parent;
            }

        }
        foreach (GameObject s in Asteroids)
        {
            //s.transform.position -= transform.position;
            s.transform.position = Vector3.MoveTowards(s.transform.position, transform.position, 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 100);
    }
}
