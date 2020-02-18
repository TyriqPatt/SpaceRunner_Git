using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public GameObject[] Asteriods;
    public List<GameObject> a = new List<GameObject>();
    public GameObject Asteriod;
    public LayerMask layer;
    public bool IsPulling;
    float duration;
    // Start is called before the first frame update
    void OnEnable()
    {
        duration = 0;
        IsPulling = true;
        //Asteriods = GameObject.FindGameObjectsWithTag("Asteroid");

        //Asteriods = hit[hit.Length].transform.gameObject;
        //foreach (GameObject s in hit)
        //{

        //    //s.transform.position -= transform.position;
        //    s.transform.position = Vector3.MoveTowards(s.transform.position, transform.position, 1);
        //}
        //if (hit.transform.tag == "Asteroid")
        //{
        //    Asteroids.Add(hit.transform.gameObject);
        //    hit.transform.parent = transform.parent;
        //}


    }

    // Update is called once per frame
    void Update()
    {
        //Asteriod.transform.position = Vector3.MoveTowards(Asteriod.transform.position, transform.position, 1);

        //RaycastHit[] hit = Physics.SphereCastAll(transform.position, 100, transform.forward, 10);
        //a.Add(hit[hit.Length].transform.gameObject);
        if (IsPulling)
        {
            duration += Time.deltaTime;
            if (duration >= 3)
            {
                GetComponent<BlackHole>().enabled = false;
            }
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, 100.0f, Vector3.forward, 10, layer);
            if (hits.Length > 0)
            {
                Asteriods = new GameObject[hits.Length];
                for (int i = 0; i < hits.Length; i++)
                {
                    Asteriods[i] = hits[i].collider.gameObject;
                }
                foreach (GameObject s in Asteriods)
                {
                    //s.transform.position -= transform.position;
                    s.transform.position = Vector3.MoveTowards(s.transform.position, transform.position, .5f);
                }
            }
        }
        

    }

    private void OnDisable()
    {
        IsPulling = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 100);
    }
}
